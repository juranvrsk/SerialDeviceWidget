using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialDeviceWidget
{
    public class SerialDevice : IEquatable<SerialDevice>, IComparable<SerialDevice>
    {
        public SerialDevice(string deviceName, bool hidden) 
        { 
            Name = deviceName;
            //Port = deviceName.Where(deviceName => deviceName.Contains("COM\d+"));
            //A search of the "(COMXX)" substring between the last index of the "(" + "COM" and last index of the ")"
            int fIndex = deviceName.LastIndexOf('(')+4;
            int lIndex = deviceName.LastIndexOf(')');
            Port = int.Parse(deviceName.Substring(fIndex, lIndex-fIndex));            
            Hidden = hidden;
        }
        public string Name { get; private set; }
        public int Port { get; private set; }
        public bool Hidden { get; set; }

        public bool Equals(SerialDevice other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }

        public int CompareTo(SerialDevice compareDevice)
        {
            // A null value means that this object is greater.
            if (compareDevice == null)
                return 1;

            else
                return this.Name.CompareTo(compareDevice.Name);
        }

        public int SortByNameAscending(string name1, string name2)
        {

            return name1.CompareTo(name2);
        }

        public int SortByNameDescending(string name1, string name2)
        {

            return name2.CompareTo(name2);
        }

        /*public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SerialDevice serialDeviceObj = obj as SerialDevice;
            if (serialDeviceObj == null) return false;
            else return Equals(serialDeviceObj);
        }*/



        //public int SortByNameAscending()
    }
}
