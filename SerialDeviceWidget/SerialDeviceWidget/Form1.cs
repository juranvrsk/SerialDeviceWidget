using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Threading;
using System.Reflection;


namespace SerialDeviceWidget
{
    public partial class FormMain : Form
    {
        public Model model;
        public BindingList<SerialDevice> bindingList;
        private bool itemsChecked;
        private ManagementEventWatcher mWatcher;
        private ToolStripMenuItem toolStripMenuItemExit;
        private ToolStripSeparator ToolStripSeparatorBottom;
        private const string USBGuidString = "{4d36e978-e325-11ce-bfc1-08002be10318}";

        public FormMain(Model model)
        {
            this.model = model;
            InitializeComponent();
            toolStripMenuItemExit = new ToolStripMenuItem();
            toolStripMenuItemExit.Text = "Exit";
            toolStripMenuItemExit.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItemExit.Click += new System.EventHandler(toolStripMenuItemExit_Click);
            ToolStripSeparatorBottom = new ToolStripSeparator();
            ToolStripSeparatorBottom.Size = new System.Drawing.Size(177, 6);
            
            this.bindingList = new BindingList<SerialDevice>(model.GetSerialDevices());
            model.RefreshRate = trackBarRefreshRate.Value;
            //checkedListBoxSerialDevices.DataSource = bindingList;
            //checkedListBoxSerialDevices.DisplayMember = "Name";
            dataGridViewSerialDevices.DataSource = bindingList;
            dataGridViewSerialDevices.AllowUserToAddRows = false;
            dataGridViewSerialDevices.AllowUserToDeleteRows = false;
            model.SerialListUpdated += ModelSerialDeviceListUpdated;
            labelRefresh.Text += model.GetRefreshString();
            EnumarateCOMPortsWMI();            
            UpdateToolStripMenu();
            InsertUSBHandler();
            RemoveUSBHandler();            
        }

        private void ModelSerialDeviceListUpdated(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Windows.Forms.MethodInvoker(()=> bindingList.ResetBindings()));
                return;
            }
            bindingList.ResetBindings();
            UpdateToolStripMenu();
        }

        private bool CheckAll()
        {
            /*for(int i = 0; i < checkedListBoxSerialDevices.Items.Count; i++)
            {
                checkedListBoxSerialDevices.SetItemChecked(i, true);
            }*/
            foreach (DataGridViewRow item in dataGridViewSerialDevices.Rows)
            {
                item.Cells["Hidden"].Value = true;
            }
            return true;
        }

        private bool UnCheckAll()
        {
            foreach (DataGridViewRow item in dataGridViewSerialDevices.Rows)
            {
                item.Cells["Hidden"].Value = false;
            }
            /*for (int i = 0; i < checkedListBoxSerialDevices.Items.Count; i++)
            {
                checkedListBoxSerialDevices.SetItemChecked(i, false);
            }*/
            return false;
        }

        /*private void SetCheckUncheckItems()
        {
            for (int i = 0; i < checkedListBoxSerialDevices.Items.Count; i++)
            {
                if (checkedListBoxSerialDevices.Items[i] is SerialDevice device)
                {
                    checkedListBoxSerialDevices.SetItemChecked(i, !device.Hidden);
                }
            }
        }*/

        private void UpdateToolStripMenu()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Windows.Forms.MethodInvoker(UpdateToolStripMenu));
                return;
            }
            contextMenuStripMain.Items.Clear();
            //SetCheckUncheckItems();
            /*foreach (SerialDevice device in checkedListBoxSerialDevices.CheckedItems)
            {
                contextMenuStripMain.Items.Add(device.Name);
            }*/

            /*var list = dataGridViewSerialDevices.Rows.Cast<DataGridViewRow>()
                .SelectMany( row => row.Cells.Cast<DataGridViewCell>())
                .Where( x => x.va)
                .ToList();*/
            //dataGridViewSerialDevices.DataSource is List<SerialDevice> devices;

            /*var list = bindingList.Select(d =>
            {
                d.Hidden = true;
                return d;
            }).ToList();
            bindingList = new BindingList<SerialDevice>(list);*/
            string[] notHidden = bindingList.Where(x => x.Hidden==false).Select(y => y.Name).ToArray();
            ToolStripItem[] toolStripItems = notHidden.Select(x => new ToolStripMenuItem(x) as ToolStripItem).ToArray();            
            contextMenuStripMain.Items.AddRange(toolStripItems);
            contextMenuStripMain.Items.Add(ToolStripSeparatorBottom);
            contextMenuStripMain.Items.Add(toolStripMenuItemExit);
            
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {            
            notifyIconMain.Visible = false;
            Application.Exit();
        }

        private void AddDevice(string deviceName)
        {                      
            SerialDevice device = new SerialDevice(deviceName, false);
            model.AddSerialDevice(device);
            UpdateToolStripMenu();
        }

        private void RemoveDevice(string deviceName) 
        {            
            SerialDevice device = new SerialDevice(deviceName, false);
            model.RemoveSerialDevice(device);
            UpdateToolStripMenu();
        }

        private void EnumarateCOMPortsWMI()
        {
            model.ClearSerialDeviceList();
            try
            {
                //Choosing from the Win32 WMI list of the PnP devices, which contains the COM ports
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM[0-9]%'");

                foreach (ManagementObject queryObj in searcher.Get())//Took all port names from query result
                {
                    SerialDevice device = new SerialDevice(queryObj["Caption"].ToString(), false);
                    model.AddSerialDevice(device);
                }
            }
            catch (ManagementException e) //In case of error output error message to devices list
            {
                var portMenuItem = new ToolStripMenuItem("An error occurred while querying for WMI data: " + e.Message);
                contextMenuStripMain.Items.Add(portMenuItem);
            }
            itemsChecked = UnCheckAll();
        }

        private void buttonCheckAll_Click(object sender, EventArgs e)
        {
            if(itemsChecked)
            {
                itemsChecked = UnCheckAll();
            }
            else
            {
                itemsChecked = CheckAll();
            }
            UpdateToolStripMenu();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            /*for (int i = 0; i < checkedListBoxSerialDevices.Items.Count; i++)
            {
                if (checkedListBoxSerialDevices.Items[i] is SerialDevice device)
                {
                    checkedListBoxSerialDevices.SetItemChecked(i, !device.Hidden);
                }
            }*/
            UpdateToolStripMenu();
        }

        private void trackBarRefreshRate_ValueChanged(object sender, EventArgs e)
        {
            model.RefreshRate = trackBarRefreshRate.Value;
            labelRefresh.Text = $"Refresh rate: {model.GetRefreshString()}";
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void notifyIconMain_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Hide();
            }
            else
            { 
                Show(); 
            }            
        }

        private void InsertUSBHandler()
        {
            WqlEventQuery eventQuery;
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;
            try
            {
                eventQuery = new WqlEventQuery();
                eventQuery.EventClassName = "__InstanceCreationEvent";
                eventQuery.WithinInterval = new TimeSpan(0, 0, 1);//Set up seconds from the settings or enter the new variable
                eventQuery.Condition = "TargetInstance ISA 'Win32_PnPEntity'";
                mWatcher = new ManagementEventWatcher(scope, eventQuery);
                mWatcher.EventArrived += new EventArrivedEventHandler(USBInserted);
                mWatcher.Start();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                if(mWatcher != null)
                {
                    mWatcher.Stop();
                }
            }
        }

        private void USBInserted(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject baseObject = (ManagementBaseObject)e.NewEvent["TargetInstance"];//Getting the data from query         
            if ((string)baseObject["ClassGuid"] == "{4d36e978-e325-11ce-bfc1-08002be10318}")
            {
                string deviceName = baseObject["Caption"].ToString();
                LaunchToastNotification("New serial device added", deviceName);
                AddDevice(deviceName);
            }            
        }

        private void RemoveUSBHandler()
        {
            WqlEventQuery eventQuery;
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;
            try
            {
                eventQuery = new WqlEventQuery();
                eventQuery.EventClassName = "__InstanceDeletionEvent";
                eventQuery.WithinInterval = new TimeSpan(0, 0, 1);//Set up seconds from the settings or enter the new variable
                eventQuery.Condition = "TargetInstance ISA 'Win32_PnPEntity'";
                mWatcher = new ManagementEventWatcher(scope, eventQuery);
                mWatcher.EventArrived += new EventArrivedEventHandler(USBRemoved);
                mWatcher.Start();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                if(mWatcher != null)
                {
                    mWatcher.Stop();
                }
            }            
        }

        private void USBRemoved(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject baseObject = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            if ((string)baseObject["ClassGuid"] == "{4d36e978-e325-11ce-bfc1-08002be10318}")
            {
                string deviceName = baseObject["Caption"].ToString();
                RemoveDevice(deviceName);
            }
        }

        private void LaunchToastNotification(string tilte, string description)
        {
            new ToastContentBuilder()
                .AddText(tilte)
                .AddText(description)
                .Show();
        }

    }    
}
