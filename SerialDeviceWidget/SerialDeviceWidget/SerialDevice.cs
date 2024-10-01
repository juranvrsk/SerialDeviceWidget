using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SerialDeviceWidget
{
    public class SerialDevice 
    {
        public SerialDevice(string deviceName, bool hidden) 
        { 
            Name = deviceName;

            //A search of the "COMXX" pattern and extracting digits only.
            //Done with a positive lookbehind assertion,"COM" sould be preceed the digits, but it excludes from match
            Regex regex = new Regex(@"(?<=COM)\d+");
            if (regex.IsMatch(Name)) 
            { 
                Port = int.Parse(regex.Match(Name).Value);
            }
            else
            {
                Port = 0;
            }
            MatchCollection matches = regex.Matches(Name);      
            
            Hidden = hidden;
        }
        public string Name { get; private set; }
        public int Port { get; private set; }
        public bool Hidden { get; set; }

    }
}
