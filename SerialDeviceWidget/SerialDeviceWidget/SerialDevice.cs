using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialDeviceWidget
{
    public class SerialDevice
    {
        public bool mHidden;
        public SerialDevice(string deviceName, bool hidden) 
        { 
            Name = deviceName;
            mHidden = hidden;
        }
        public string Name { get; private set; }
        public void Hide()
        {
            mHidden = true;
        }
        public void Show() 
        {
            mHidden = false;
        }

    }
}
