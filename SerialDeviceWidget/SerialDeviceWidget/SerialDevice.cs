using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialDeviceWidget
{
    public class SerialDevice 
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

    }
}
