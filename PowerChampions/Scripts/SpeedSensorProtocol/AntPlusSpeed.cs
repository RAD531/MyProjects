using ANT_Managed_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntPlusSpeed
{
    private AntChannel backgroundScanChannel;
    private int lastChannelUsed = 0;

    public List<BikeDisplayNew> bikeDisplayList = new List<BikeDisplayNew>();

    public void StartScan()
    {
        Debug.Log("starting scan");
        backgroundScanChannel = AntManager.Instance.OpenBackgroundScanChannel(0);
        backgroundScanChannel.onReceiveData += ReceivedBackgroundScanData;
    }

    void ReceivedBackgroundScanData(Byte[] data)
    {

        byte deviceType = (data[12]); // extended info Device Type byte
                                      //use the Extended Message Formats to identify nodes

        switch (deviceType)
        {
            //if its just a speed sensor or if its a speed (or) cadence sensor
            case AntplusDeviceType.BikeSpeed:
                {

                    BikeDisplayNew b = new BikeDisplayNew();

                    b.deviceNumber = (data[10]) | data[11] << 8;
                    byte transType = data[13];

                    foreach (BikeDisplayNew d in bikeDisplayList)
                    {
                        if (d.deviceNumber == (int)((data[10]) | data[11] << 8))
                            return;
                    }

                    BikeDisplayNew foundDevice = new BikeDisplayNew();
                    foundDevice.deviceType = deviceType;
                    foundDevice.deviceNumber = b.deviceNumber;
                    foundDevice.transType = transType;
                    foundDevice.period = 8118;
                    foundDevice.radiofreq = 57;
                    foundDevice.name = "BikeSpeed(" + foundDevice.deviceNumber + ")";
                    foundDevice.SetProtocol(BIKEPROTOCOL.ANTPLUS);

                    bikeDisplayList.Add(foundDevice);

                    Debug.Log("found bike speed #" + bikeDisplayList.Count);

                    break;
                }

            //if its a speed (and) cadence sensor
            case AntplusDeviceType.BikeSpeedCadence:
                {
                    int deviceNumber = (data[10]) | data[11] << 8;
                    byte transType = data[13];

                    foreach (BikeDisplayNew d in bikeDisplayList)
                    {
                        if (d.deviceNumber == transType) //device already found
                            return;
                    }

                    Debug.Log("Speed & Cadence sensor found " + deviceNumber);

                    BikeDisplayNew foundDevice = new BikeDisplayNew();
                    foundDevice.deviceType = deviceType;
                    foundDevice.deviceNumber = deviceNumber;
                    foundDevice.transType = transType;
                    foundDevice.period = 8086;
                    foundDevice.radiofreq = 57;
                    foundDevice.name = "BikeSpeedCadence(" + foundDevice.deviceNumber + ")";
                    foundDevice.SetProtocol(BIKEPROTOCOL.ANTPLUS);

                    bikeDisplayList.Add(foundDevice);

                    break;
                }

            default:
                {
                    break;
                }
        }

    }

    public void ConnectTospeed(BikeDisplayNew b)
    {
        Debug.Log("opening channel");
        lastChannelUsed++;
        AntChannel speedChannel = AntManager.Instance.OpenChannel(ANT_ReferenceLibrary.ChannelType.BASE_Slave_Receive_0x00, (byte)lastChannelUsed, (ushort)b.deviceNumber, b.deviceType, b.transType, (byte)b.radiofreq, (ushort)b.period, false); //bike speed Display
        speedChannel.onReceiveData += b.GetSpeed;
        speedChannel.hideRXFAIL = true;
    }
}
