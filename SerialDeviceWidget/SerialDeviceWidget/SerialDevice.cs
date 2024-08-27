using System;
using System.Collections.Generic;
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
            Hidden = hidden;
        }
        public string Name { get; private set; }
        public bool Hidden { get; set; }        
    }
}
