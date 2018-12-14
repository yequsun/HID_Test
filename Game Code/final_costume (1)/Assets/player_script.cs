using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HidLibrary;
using System.Linq;
using System;

public class player_script : MonoBehaviour {
    float offsetY = 0;
    float xp = 0;
    float yp = 0;
    float zp = 0;

    bool rotateWithMouse = false;

    HidLibrary.HidDevice[] HidDeviceList;
    HidDevice HidDevice;

    float x, y, z, qw, qx, qy, qz;

    HidDeviceData InData;

    // Use this for initialization
    void Start () {
        //Teensy Board vid 0x16c0, pid 0x0486
        HidDeviceList = HidDevices.Enumerate(0x16C0, 0x0486).Cast<HidDevice>().ToArray();

        if (HidDeviceList.Length > 0)
        {
            HidDevice = HidDeviceList[0];
           print("Connected: " + HidDevice.IsConnected.ToString());
            /*
             * send data to hid device
            byte[] OutData = new byte[HidDevice.Capabilities.OutputReportByteLength - 1];
            OutData[0] = 0x00C9;
            OutData[1] = 0x00C9;
            HidDevice.Write(OutData);
            */

        }

    }

    public void movePlayer(float x, float y, float z, float qw = 0, float qx = 0, float qy = 0, float qz = 0)
    {
        //this.gameObject.transform.rotation = Quaternion.Euler(y, x, -1*z);
        this.gameObject.transform.rotation = new Quaternion(-1 * qy, -1 * qz, qx, qw);
        //this.gameObject.transform.Rotate(new Vector3(x, y, z));    }
    }
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotateWithMouse = !rotateWithMouse;
        }
        if (!rotateWithMouse)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                xp += 10.0f;
                movePlayer(xp, yp, zp);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                xp -= 10.0f;
                movePlayer(xp, yp, zp);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                zp += 10.0f;
                movePlayer(xp, yp, zp);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                zp -= 10.0f;
                movePlayer(xp, yp, zp);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                yp += 10.0f;
                movePlayer(xp, yp, zp);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                yp -= 10.0f;
                movePlayer(xp, yp, zp);
            }
        }
        else
        {
            float h = 2.0f * Input.GetAxis("Mouse X");
            float v = 2.0f * Input.GetAxis("Mouse Y");
            transform.Rotate(v, h, 0);
        }
        */

            InData = HidDevice.Read();
        Byte[] InDataByteArray = InData.Data;

        //Euler xyz
        x = BitConverter.ToSingle(InDataByteArray, 1);
        y = BitConverter.ToSingle(InDataByteArray, 5);
        z = BitConverter.ToSingle(InDataByteArray, 9);

        //quat wxyz
        qw = BitConverter.ToSingle(InDataByteArray, 13);
        qx = BitConverter.ToSingle(InDataByteArray, 17);
        qy = BitConverter.ToSingle(InDataByteArray, 21);
        qz = BitConverter.ToSingle(InDataByteArray, 25);
        //print(x.ToString() + " " + y.ToString() + " " + z.ToString() + " " + qw.ToString() + " " + qx.ToString() + " " + qy.ToString() + " " + qz.ToString());

        movePlayer(x, y, z, qw, qx, qy, qz);

    }
}
