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
        private bool sortDirection;
        private ManagementEventWatcher mWatcher;
        private ToolStripMenuItem toolStripMenuItemExit;
        private ToolStripSeparator ToolStripSeparatorBottom;
        private const string USBGuidString = "{4d36e978-e325-11ce-bfc1-08002be10318}";
        //private BindingSource source;

        public FormMain(Model model)
        {
            model.RefreshRate = 1;
            sortDirection = false; //Descending
            this.model = model;
            //source = new BindingSource();
            InitializeComponent();
            toolStripMenuItemExit = new ToolStripMenuItem();
            toolStripMenuItemExit.Text = "Exit";
            toolStripMenuItemExit.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItemExit.Click += new System.EventHandler(toolStripMenuItemExit_Click);
            ToolStripSeparatorBottom = new ToolStripSeparator();
            ToolStripSeparatorBottom.Size = new System.Drawing.Size(177, 6);
            this.bindingList = new BindingList<SerialDevice>(model.SerialList);
            numericUpDownRefreshRate.DataBindings.Add("Value", model, "RefreshRate", false, DataSourceUpdateMode.OnPropertyChanged);
            //source.DataSource = this.bindingList;
            //dataGridViewSerialDevices.DataSource = source;
            dataGridViewSerialDevices.DataSource = this.bindingList;
            dataGridViewSerialDevices.AllowUserToAddRows = false;
            dataGridViewSerialDevices.AllowUserToDeleteRows = false;
            dataGridViewSerialDevices.RowHeadersVisible = false;
            dataGridViewSerialDevices.Columns["Name"].Width = 300;
            dataGridViewSerialDevices.Columns["Hidden"].Width = 50;
            dataGridViewSerialDevices.Columns["Port"].Visible = false;
            dataGridViewSerialDevices.Columns["Port"].SortMode = DataGridViewColumnSortMode.Programmatic;
            model.SerialListUpdated += ModelSerialDeviceListUpdated;
            //this.bindingList.ListChanged();
            EnumarateCOMPortsWMI();
            UpdateToolStripMenu();
            InsertUSBHandler();
            RemoveUSBHandler();
        }

        private void ModelSerialDeviceListUpdated(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Windows.Forms.MethodInvoker(() => bindingList.ResetBindings()));
                return;
            }
            bindingList.ResetBindings();
            UpdateToolStripMenu();
        }

        private bool CheckAll()
        {
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
            return false;
        }



        private void UpdateToolStripMenu()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Windows.Forms.MethodInvoker(UpdateToolStripMenu));
                return;
            }
            contextMenuStripMain.Items.Clear();
            string[] notHidden = bindingList.Where(x => x.Hidden == false).Select(y => y.Name).ToArray();
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
            if (itemsChecked)
            {
                itemsChecked = UnCheckAll();
            }
            else
            {
                itemsChecked = CheckAll();
            }
            UpdateToolStripMenu();
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
                eventQuery.WithinInterval = new TimeSpan(0, 0, model.RefreshRate);//Set up seconds from the settings or enter the new variable
                eventQuery.Condition = "TargetInstance ISA 'Win32_PnPEntity'";
                mWatcher = new ManagementEventWatcher(scope, eventQuery);
                mWatcher.EventArrived += new EventArrivedEventHandler(USBInserted);
                mWatcher.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (mWatcher != null)
                {
                    mWatcher.Stop();
                }
            }
        }

        private void USBInserted(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject baseObject = (ManagementBaseObject)e.NewEvent["TargetInstance"];//Getting the data from query         
            //Ensure for the proper device adding
            if ((string)baseObject["ClassGuid"] == USBGuidString)
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
                eventQuery.WithinInterval = new TimeSpan(0, 0, model.RefreshRate);//Set up seconds from the settings or enter the new variable
                eventQuery.Condition = "TargetInstance ISA 'Win32_PnPEntity'";
                mWatcher = new ManagementEventWatcher(scope, eventQuery);
                mWatcher.EventArrived += new EventArrivedEventHandler(USBRemoved);
                mWatcher.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (mWatcher != null)
                {
                    mWatcher.Stop();
                }
            }
        }

        private void USBRemoved(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject baseObject = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            //Ensure for the proper device removing
            if ((string)baseObject["ClassGuid"] == USBGuidString)
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

        /*
         * For immediate apllying the check/uncheck
         */
        private void dataGridViewSerialDevices_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewSerialDevices.IsCurrentCellDirty)
            {
                dataGridViewSerialDevices.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /*
         * Applaying the hide/unhide from check/uncheck
         */
        private void dataGridViewSerialDevicesCell_ValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewSerialDevices.Columns["Hidden"].Index && e.RowIndex >= 0)
            {
                UpdateToolStripMenu();
            }
        }

        private void dataGridViewSerialDevices_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*Sorting are simple as possible
             *Problems: the binded datagridview is impossible to sort using build-in automatic and programmatic sort
             *The List<T> and BindingList<T> could be sorted and returns IOrderedEnumerable<T> which cant be 
             *passed to List<T> or BindingList<T>, even using ToList<T>. Only one way is available now: clean List<T> and readd there elemnts
             *from the IOrderedEnumerable<T>.
             */
            if (e.ColumnIndex == dataGridViewSerialDevices.Columns["Name"].Index) 
            {   
                model.SortSerialDevices(sortDirection);
                sortDirection = !sortDirection;
            }
        }
    }
}
