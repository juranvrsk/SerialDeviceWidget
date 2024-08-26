using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialDeviceWidget
{
    public class Model
    {
        public event EventHandler SerialListUpdated;
        public event EventHandler RefreshRateUpdated;
        private List<SerialDevice> serialList = new List<SerialDevice>();
        public int RefreshRate { get; set; }

        public string RefreshPeriod 
        { 
            get
            {
                return GetRefreshString();
            }
        }
        public int RefreshMillis
        { 
            get
            {
                return GetRefreshMillis();
            }
        }

        private List<string> portNames = new List<string>();

        public List<SerialDevice> GetSerialDevices()
        {
            return serialList;
        }

        public void AddSerialDevice(SerialDevice device)
        {
            serialList.Add(device);
            SerialListUpdated?.Invoke(this, EventArgs.Empty);
        }
        
        public void RemoveSerialDevice(SerialDevice device)
        {
            serialList.Remove(device);
            SerialListUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void ClearSerialDeviceList()
        {
            serialList.Clear();
            SerialListUpdated?.Invoke(this, EventArgs.Empty);
        }

        public string AddPortName(int portNumber)
        {
            string name = string.Format("Port {0}", portNumber);
            portNames.Add(name);
            return name;
        }

        public void ClearPortNames()
        {
            portNames.Clear();
        }

        public List<string> GetPortNames()
        {
            return portNames;
        }

        public int GetRefreshMillis()
        {
            switch (RefreshRate)
            {
                case 1:  return 1000;
                case 2:  return 3 * 1000;
                case 3:  return 5 * 1000;
                case 4:  return 10 * 1000;
                case 5:  return 30 * 1000;
                case 6:  return 60 * 1000;
                case 7:  return 3 * 60 * 1000;
                case 8:  return 5 * 60 * 1000;
                case 9:  return 10 * 60 * 1000;
                case 10: return 30 * 60 * 1000;
                default: return 30 * 60 * 1000;
            }
        }

        public string GetRefreshString()
        {
            switch (RefreshRate)
            {
                case 1:  return "   1 s";
                case 2:  return "   3 s";
                case 3:  return "   5 s";
                case 4:  return "  10 s";
                case 5:  return "  30 s";
                case 6:  return "1  min";
                case 7:  return "3  min";
                case 8:  return "5  min";
                case 9:  return "10 min";
                case 10: return "30 min";
                default: return "30 min";
            }
        }
    }
}
