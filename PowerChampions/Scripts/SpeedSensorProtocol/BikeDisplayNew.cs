using System;
using System.Collections;
using System.Collections.Generic;

public class BikeDisplayNew
{
    //Ant+
    public byte deviceType;
    public byte transType;
    public int period;
    public int deviceNumber;
    public int radiofreq;

    //BLE
    public string deviceId;
    public string serviceUuid;
    public string[] characteristicUuids = new string[1];

    //Both
    private BIKEPROTOCOL protocol;

    public string name;

    public float speed;
    public float distance;
    //variable use for speed display
    int prevRev;
    int prevMeasTime = 0;

    int stoppedCounter = 0;
    private int stopRevCounter_speed = 0;
    private int prev_measTime_speed = 0;
    private int prev_revCount_speed = 0;

    private int revCountZero = 0;
    public float wheelCircumference = 2.05f;
    public bool isConnected = false;

    //packetindexes
    private int packetIndex1;
    private int packetIndex2;
    private int packetIndex3;
    private int packetIndex4;

    public void SetProtocol(BIKEPROTOCOL _protocol)
    {
        if (_protocol == BIKEPROTOCOL.BLE)
        {
            protocol = BIKEPROTOCOL.BLE;

            packetIndex1 = 5;
            packetIndex2 = 6;
            packetIndex3 = 1;
            packetIndex4 = 2;
        }

        else if (_protocol == BIKEPROTOCOL.ANTPLUS)
        {
            protocol = BIKEPROTOCOL.ANTPLUS;

            packetIndex1 = 4;
            packetIndex2 = 5;
            packetIndex3 = 6;
            packetIndex4 = 7;
        }
    }

    public BIKEPROTOCOL GetProtocol()
    {
        return protocol;
    }


    public void GetSpeed(Byte[] data)
    {

        int measTime_speed = (data[packetIndex1]) | data[packetIndex2] << 8;
        int revCount_speed = (data[packetIndex3]) | data[packetIndex4] << 8;


        if (prev_measTime_speed != 0 && measTime_speed != prev_measTime_speed && prev_measTime_speed < measTime_speed && prev_revCount_speed < revCount_speed)
        {
            speed = (wheelCircumference * (revCount_speed - prev_revCount_speed) * 1024) / (measTime_speed - prev_measTime_speed);
            speed *= 3.6f; // km/h
            stopRevCounter_speed = 0;

        }
        else
            stopRevCounter_speed++;

        if (stopRevCounter_speed >= 5)
        {
            stopRevCounter_speed = 5;
            speed = 0;
        }


        prev_measTime_speed = measTime_speed;
        prev_revCount_speed = revCount_speed;

        //DISTANCE
        if (revCountZero == 0)
            revCountZero = revCount_speed;

        distance = wheelCircumference * (revCount_speed - revCountZero);
    }
}

public enum BIKEPROTOCOL
{
    ANTPLUS,
    BLE
}
