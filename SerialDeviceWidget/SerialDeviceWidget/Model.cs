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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnProertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
            //serialList.Sort();
            serialList = (from sd in serialList orderby sd.Name descending select sd).ToList();
            SerialListUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
