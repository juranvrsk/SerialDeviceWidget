# SerialDeviceWidget
An application for monitoring connected serial devices. I made it for comfortable work with the several serial devices (like MCU, USB-TTL, etc.) simultaneously.

There are three ways to look a serial port list in this application:
1. The widget-like window, where possible to order the ports by its number, hide the useless ports and set up refresh period for the usb poll
2. The notify icon menu, clickable(left mouse button)
3. The toast notifications for the newly inserted devices.

Works on windows 10.
Written in C#, .NET8 and WinForms

# The working principe
1. The already inserted devices is enumerated using WMI .NET tools at the application start
2. The newly inserted or removed devices are handled as events by WMI .NET tools
3. The refresh period for WMI events could be changed
4. The insertion event cause the windows toast notification

Why the WMI? Because:
1. System.IO.Ports.SerialPort.GetPortNames() rteturns only port numbers, not the port names or descriptions.
2. Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM") - does not providing the device name, only the com port numbers.
3. The UWP Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(SerialDevice.GetDeviceSelector()) sometimes does not properly returns the device name.
So WMI is optimal for me.
