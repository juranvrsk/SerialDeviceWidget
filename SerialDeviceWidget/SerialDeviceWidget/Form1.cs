﻿using System;
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
        public FormMain(Model model)
        {
            this.model = model;
            InitializeComponent();
            this.bindingList = new BindingList<SerialDevice>(model.GetSerialDevices());
            model.RefreshRate = trackBarRefreshRate.Value;
            checkedListBoxSerialDevices.DataSource = bindingList;
            checkedListBoxSerialDevices.DisplayMember = "Serial Device";
            model.SerialListUpdated += ModelSerialDeviceListUpdated;
            labelRefresh.Text += model.GetRefreshString();
            timerRefresh.Start();
            unCheckedItems = new List<object>();
            EnumarateCOMPortsWMI();            
            UpdateToolStripMenu();
            InsertUSBHandler();
            RemoveUSBHandler();
        }

        private void ModelSerialDeviceListUpdated(object sender, EventArgs e)
        {
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

        private void UpdateToolStripMenu()
        {
            ClearToolStripMenu();
            SetUncheckedItems(unCheckedItems);
            //SetUncheckedItems(GetUnCheckedItems());
            int portCounter = 1;
            foreach (string item in checkedListBoxSerialDevices.CheckedItems)
            {
                portCounter++;
                var portMenuItem = new ToolStripMenuItem(item);                
                portMenuItem.Name = model.AddPortName(portCounter);
                contextMenuStripMain.Items.Insert(portCounter, portMenuItem);
            }
        }

        private void ClearToolStripMenu()
        {
            foreach(string name in model.GetPortNames())
            {
                contextMenuStripMain.Items.RemoveByKey(name);
            }
            model.ClearPortNames();
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {            
            timerRefresh.Stop();
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
            RefreshEnumeration();
        }

        private void RefreshEnumeration()
        {
            unCheckedItems = GetUnCheckedItems();
            bindingList.Clear();
            EnumarateCOMPortsWMI();            
            UpdateToolStripMenu();           
        }

        private void AddDevice(string deviceName)
        {
            unCheckedItems = GetUnCheckedItems();
            bindingList.Clear();
            model.AddSerialDevice(deviceName);
            UpdateToolStripMenu();
        }

        private void RemoveDevice(string deviceName) 
        {
            unCheckedItems = GetUnCheckedItems();
            bindingList.Clear();
            model.RemoveSerialDevice(deviceName);
            UpdateToolStripMenu();
        }

        private List<object> GetUnCheckedItems()
        {
            List<object> result = new List<object>();
            foreach(object item in checkedListBoxSerialDevices.Items)
            {                
                if(!checkedListBoxSerialDevices.GetItemChecked(checkedListBoxSerialDevices.Items.IndexOf(item)))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        private void SetUncheckedItems(List<object> uncheckedList)
        {
            foreach (object item in uncheckedList)
            {
                checkedListBoxSerialDevices.SetItemChecked(checkedListBoxSerialDevices.Items.IndexOf(item), false);
            }
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
                    string res = String.Format("{0}", queryObj["Caption"]);
                    model.AddSerialDevice(res);
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
            timerRefresh.Interval = model.GetRefreshMillis();
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
            string deviceName = baseObject["Caption"].ToString();
            LaunchToastNotification("New serial device added", deviceCaption);
            AddDevice(deviceName);
            //RefreshEnumeration();
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
            string deviceName = baseObject["Caption"].ToString();
            RemoveDevice(deviceName);
            //RefreshEnumeration();
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
