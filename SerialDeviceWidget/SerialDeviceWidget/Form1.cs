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
//using Windows.Devices.SerialCommunication;
//using Windows.Devices.Enumeration;
//using Windows.Storage.Streams;
//using System.IO.Ports;


namespace SerialDeviceWidget
{
    public partial class FormMain : Form
    {
        public List<String> PortNames;//The list for port names store
        public Model model;
        public BindingList<SerialDevice> bindingList;
        private bool itemsChecked;
        private List<object> unCheckedItems;
        private ManagementEventWatcher mWatcher;
        private ToolStripMenuItem toolStripMenuItemRefresh;
        private ToolStripMenuItem toolStripMenuItemExit;
        private ToolStripSeparator toolStripSeparatorTop;
        private ToolStripSeparator ToolStripSeparatorBottom;

        public FormMain(Model model)
        {
            this.model = model;
            InitializeComponent();
            toolStripMenuItemRefresh = new ToolStripMenuItem();
            toolStripMenuItemRefresh.Text = "Refresh";
            toolStripMenuItemRefresh.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItemRefresh.Click += new System.EventHandler(toolStripMenuItemRefresh_Click);
            toolStripMenuItemExit = new ToolStripMenuItem();
            toolStripMenuItemExit.Text = "Exit";
            toolStripMenuItemExit.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItemExit.Click += new System.EventHandler(toolStripMenuItemExit_Click);
            toolStripSeparatorTop = new ToolStripSeparator();
            toolStripSeparatorTop.Size = new System.Drawing.Size(177, 6);
            ToolStripSeparatorBottom = new ToolStripSeparator();
            ToolStripSeparatorBottom.Size = new System.Drawing.Size(177, 6);
            
            this.bindingList = new BindingList<SerialDevice>(model.GetSerialDevices());
            model.RefreshRate = trackBarRefreshRate.Value;
            checkedListBoxSerialDevices.DataSource = bindingList;
            checkedListBoxSerialDevices.DisplayMember = "Name";            
            model.SerialListUpdated += ModelSerialDeviceListUpdated;
            labelRefresh.Text += model.GetRefreshString();
            unCheckedItems = new List<object>();
            contextMenuStripMain.Items.Add(toolStripMenuItemRefresh);
            contextMenuStripMain.Items.Add(toolStripSeparatorTop);
            EnumarateCOMPortsWMI();            
            UpdateToolStripMenu();
            InsertUSBHandler();
            RemoveUSBHandler();
            contextMenuStripMain.Items.Add(ToolStripSeparatorBottom);
            contextMenuStripMain.Items.Add(toolStripMenuItemExit);            
        }

        private void ModelSerialDeviceListUpdated(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Windows.Forms.MethodInvoker(()=> bindingList.ResetBindings()));
                return;
            }
            bindingList.ResetBindings();
        }

        private bool CheckAll()
        {            
            for(int i = 0; i < checkedListBoxSerialDevices.Items.Count; i++)
            {
                checkedListBoxSerialDevices.SetItemChecked(i, true);
            }
            return true;
        }

        private bool UnCheckAll()
        {
            for (int i = 0; i < checkedListBoxSerialDevices.Items.Count; i++)
            {
                checkedListBoxSerialDevices.SetItemChecked(i, false);
            }
            return false;
        }

        private void SetCheckUncheckItems()
        {
            for (int i = 0; i < checkedListBoxSerialDevices.Items.Count; i++)
            {
                if (checkedListBoxSerialDevices.Items[i] is SerialDevice device)
                {
                    checkedListBoxSerialDevices.SetItemChecked(i, !device.Hidden);
                }
            }
        }

        private void UpdateToolStripMenu()
        {
            //ClearToolStripMenu();
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Windows.Forms.MethodInvoker(UpdateToolStripMenu));
                return;
            }
            contextMenuStripMain.Items.Clear();
            SetCheckUncheckItems();
            contextMenuStripMain.Items.Add(toolStripMenuItemRefresh);
            contextMenuStripMain.Items.Add(toolStripSeparatorTop);
            foreach (SerialDevice device in checkedListBoxSerialDevices.CheckedItems)
            {
                contextMenuStripMain.Items.Add(device.Name);
            }
            contextMenuStripMain.Items.Add(ToolStripSeparatorBottom);
            contextMenuStripMain.Items.Add(toolStripMenuItemExit);
            
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {            
            notifyIconMain.Visible = false;
            //base.OnFormClosing(e);
            Application.Exit();
        }

        private void toolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            RefreshEnumeration();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            //RefreshEnumeration();
            RemoveDevice("Silicon Labs CP210x USB to UART Bridge (COM6)");
        }

        private void RefreshEnumeration()
        {            
            //bindingList.Clear();
            EnumarateCOMPortsWMI();            
            UpdateToolStripMenu();           
        }

        private void AddDevice(string deviceName)
        {            
            //bindingList.Clear();            
            SerialDevice device = new SerialDevice(deviceName, false);
            model.AddSerialDevice(device);
            UpdateToolStripMenu();
        }

        private void RemoveDevice(string deviceName) 
        {            
            //bindingList.Clear();
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
            itemsChecked = CheckAll();
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
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            UpdateToolStripMenu();
        }

        private void trackBarRefreshRate_ValueChanged(object sender, EventArgs e)
        {
            model.RefreshRate = trackBarRefreshRate.Value;
            labelRefresh.Text = $"Refresh rate: {model.GetRefreshString()}";
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            RefreshEnumeration();
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
            //if(new Guid((string)baseObject["ClassGuid"])==GUID_DEVCLASS_USB            
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
