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

namespace SerialDeviceWidget
{
    public partial class FormMain : Form
    {
        public List<String> PortNames;//The list for port names store

        public FormMain()
        {
            InitializeComponent();
            PortNames = new List<string>();
            EnumarateCOMPortsWMI();
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
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
            foreach (String portName in PortNames) //Clearing of the list of serial ports
            {
                contextMenuStripMain.Items.RemoveByKey(portName);
                listViewSerial.Items.RemoveByKey(portName);
            }
            PortNames.Clear();
            EnumarateCOMPortsWMI();
        }

        private void EnumarateCOMPortsWMI()
        {
            try
            {
                //Choosing from the Win32 WMI list of the PnP devices, which contains the COM ports
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM[0-9]%'");

                //Add the numeration for port names
                int portCounter = 1;
                foreach (ManagementObject queryObj in searcher.Get())//Took all port names from query result
                {
                    PortNames.Add("AddedPort" + portCounter.ToString());
                    portCounter++;
                    string res = String.Format("{0}", queryObj["Caption"]);
                    var portMenuItem = new ToolStripMenuItem(res);
                    portMenuItem.Name = PortNames.Last();
                    var portListViewItem = new ListViewItem(res);
                    portListViewItem.Name = PortNames.Last();                    
                    //contextMenuStripMain.Items.Add(portMenuItem);
                    contextMenuStripMain.Items.Insert(portCounter, portMenuItem);
                    listViewSerial.Items.Add(portListViewItem);
                }
            }
            catch (ManagementException e) //In case of error output error message to devices list
            {
                var portMenuItem = new ToolStripMenuItem("An error occurred while querying for WMI data: " + e.Message);
                contextMenuStripMain.Items.Add(portMenuItem);
            }
        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            notifyIconMain.Visible = false;
            base.OnFormClosing(e);
        }

        private void buttonCheckAll_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listViewSerial.Items)
            {
                item.Checked = true;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            foreach(var item in listViewSerial.CheckedIndices)
            {
                contextMenuStripMain.Items.RemoveAt((int)item);
            }
        }

    }    
}
