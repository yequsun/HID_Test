using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using HidLibrary;
using System.Threading;

namespace HID_TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            HidLibrary.HidDevice[] HidDeviceList;
            HidDevice HidDevice;
            //Teensy Board vid 0x16c0, pid 0x0486
            HidDeviceList = HidDevices.Enumerate(0x16C0, 0x0486).Cast<HidDevice>().ToArray();

            double x, y, z;
            if (HidDeviceList.Length > 0)
            {
                HidDevice = HidDeviceList[0];
                Debug.WriteLine("Connected: " + HidDevice.IsConnected.ToString());
                /*
                 * send data to hid device
                byte[] OutData = new byte[HidDevice.Capabilities.OutputReportByteLength - 1];
                OutData[0] = 0x00C9;
                OutData[1] = 0x00C9;
                HidDevice.Write(OutData);
                */


                HidDeviceData InData;
               

                while (true)
                {
                    // read from hid device
                    InData = HidDevice.Read();
                    Byte[] InDataByteArray = InData.Data;
                    x = BitConverter.ToSingle(InDataByteArray, 1);
                    y = BitConverter.ToSingle(InDataByteArray, 5);
                    z = BitConverter.ToSingle(InDataByteArray, 9);
                    Console.WriteLine(x.ToString() + " " + y.ToString() + " " + z.ToString() + " ");


                }
            }
        }
    }
}
