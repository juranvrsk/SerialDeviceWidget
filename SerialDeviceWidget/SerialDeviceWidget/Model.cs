using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SerialDeviceWidget
{
    public class Model:INotifyPropertyChanged
    {
        public event EventHandler SerialListUpdated;
        private List<SerialDevice> serialList = new List<SerialDevice>();
        public int RefreshRate { get; set; }
        public List<SerialDevice> SerialList {  get { return serialList; } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnProertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddSerialDevice(SerialDevice device)
        {
            serialList.Add(device);
            //serialList = serialList.OrderBy(serial => serial.Port).ToList();
            SerialListUpdated?.Invoke(this, EventArgs.Empty);
        }
        
        public void RemoveSerialDevice(SerialDevice device)
        {
            SerialDevice deviceToRemove = serialList.First(dev => dev.Name == device.Name);
            serialList.Remove(deviceToRemove);
            SerialListUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void ClearSerialDeviceList()
        {
            serialList.Clear();
            SerialListUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void SortSerialDevices()
        {
            List<SerialDevice> sortedList = serialList.OrderBy(serial => serial.Port).ToList();
            serialList.Clear();
            foreach (SerialDevice serial in sortedList) 
            { 
                serialList.Add(serial);
            }        
            SerialListUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
