using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BLESpeed : MonoBehaviour
{
    public List<BikeDisplayNew> bikeDisplayList = new List<BikeDisplayNew>();

    // Change this to match your device.
    string targetDeviceName = "47749-209";
    string serviceUuid = "{00001816-0000-1000-8000-00805f9b34fb}";
    string[] characteristicUuids = {
         "{00002a5b-0000-1000-8000-00805f9b34fb}"      // CUUID 1
         //"{00002a5c-0000-1000-8000-00805f9b34fb}",      // CUUID 2
         //"{00002a5d-0000-1000-8000-00805f9b34fb}",
         //"{00002a55-0000-1000-8000-00805f9b34fb}"
    };

    BLE ble;
    BLE.BLEScan scan;
    bool isScanning = false, isConnected = false;
    string deviceId = null;
    IDictionary<string, string> discoveredDevices = new Dictionary<string, string>();
    int devicesCount = 0;

    // BLE Threads 
    Thread scanningThread, connectionThread, readingThread;

    // GUI elements
    int remoteAngle, lastRemoteAngle;

    public void StartScan()
    {
        ble = new BLE();
        readingThread = new Thread(ReadBleData);

        StartScanHandler();
    }

    // Update is called once per frame
    void Update()
    {
        if (isScanning)
        {
            if (discoveredDevices.Count > devicesCount)
            {
                UpdateGuiText("scan");
                devicesCount = discoveredDevices.Count;
            }
        }
        else
        {
            /* Restart scan in same play session not supported yet.
            if (!ButtonStartScan.enabled)
                ButtonStartScan.enabled = true;
            */
        }

        // The target device was found.
        if (deviceId != null && deviceId != "-1")
        {
            // Target device is connected and GUI knows.
            if (ble.isConnected && isConnected)
            {
                UpdateGuiText("writeData");
            }
            // Target device is connected, but GUI hasn't updated yet.
            else if (ble.isConnected && !isConnected)
            {
                UpdateGuiText("connected");
                isConnected = true;
                // Device was found, but not connected yet. 
            }
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    private void OnApplicationQuit()
    {
        CleanUp();
    }

    // Prevent threading issues and free BLE stack.
    // Can cause Unity to freeze and lead
    // to errors when omitted.
    private void CleanUp()
    {
        try
        {
            scan.Cancel();
            ble.Close();
            scanningThread.Abort();
            connectionThread.Abort();
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Thread or object never initialized.\n" + e);
        }
    }

    private void StartScanHandler()
    {
        devicesCount = 0;
        isScanning = true;
        discoveredDevices.Clear();
        scanningThread = new Thread(ScanBleDevices);
        scanningThread.Start();
    }

    public void ResetHandler()
    {
        // Reset previous discovered devices
        discoveredDevices.Clear();
        deviceId = null;
        CleanUp();
    }

    private void ReadBleData(object obj)
    {
        foreach(BikeDisplayNew bike in bikeDisplayList)
        {
            if (bike.isConnected)
            {
                byte[] packageReceived = BLE.ReadBytes();
                bike.GetSpeed(packageReceived);
            }
        }

        //GetSpeed(packageReceived);

        // Convert little Endian.
        // In this example we're interested about an angle
        // value on the first field of our package.
        //remoteAngle = packageReceived[0];
        //Debug.Log("Angle: " + remoteAngle);
        //Thread.Sleep(100);
    }

    void UpdateGuiText(string action)
    {
        switch (action)
        {
            case "scan":
                foreach (KeyValuePair<string, string> entry in discoveredDevices)
                {
                    Debug.Log("Added device: " + entry.Key);
                }
                break;
            case "writeData":
                if (!readingThread.IsAlive)
                {
                    readingThread = new Thread(ReadBleData);
                    readingThread.Start();
                }
                if (remoteAngle != lastRemoteAngle)
                {
                    lastRemoteAngle = remoteAngle;
                }
                break;
        }
    }

    void ScanBleDevices()
    {
        scan = BLE.ScanDevices();
        Debug.Log("BLE.ScanDevices() started.");

        scan.Found = (_deviceId, deviceName) =>
        {
            Debug.Log("found device with name: " + deviceName);

            if (!discoveredDevices.ContainsKey(_deviceId))
            {
                discoveredDevices.Add(_deviceId, deviceName);
            }

            //check if device matches the service i.e. speed service
            if (ble.CheckProfile(_deviceId, serviceUuid))
            {
                BikeDisplayNew bike = new BikeDisplayNew();

                bike.name = "BikeSpeed(" + deviceName + ")";
                bike.deviceId = _deviceId;
                bike.serviceUuid = serviceUuid;
                bike.characteristicUuids.SetValue(characteristicUuids[0], 0);
                bike.SetProtocol(BIKEPROTOCOL.BLE);

                bool foundDeviceInList = false;

                //check if device is not already in bike display list
                foreach(BikeDisplayNew bikeInList in bikeDisplayList)
                {
                    if (bikeInList.name == bike.name)
                    {
                        foundDeviceInList = true;
                        break;
                    }
                }

                if (!foundDeviceInList)
                {
                    bikeDisplayList.Add(bike);
                }
            }

            if (deviceId == null && deviceName == targetDeviceName)
                deviceId = _deviceId;
        };

        scan.Finished = () =>
        {
            isScanning = false;
            Debug.Log("scan finished");
            if (deviceId == null)
                deviceId = "-1";
        };

        /*
        while (deviceId == null)
            Thread.Sleep(500);
        
        scan.Cancel();
        scanningThread = null;
        isScanning = false;

        if (deviceId == "-1")
        {
            Debug.Log("no device found!");
            StartScanHandler();
            return;
        }*/
    }

    // Start establish BLE connection with
    // target device in dedicated thread.
    public void StartConHandler(BikeDisplayNew bike)
    {
        connectionThread = new Thread(() => ConnectBleDevice(bike));
        connectionThread.Start();
    }

    void ConnectBleDevice(BikeDisplayNew bike)
    {
        if (!ble.isConnected)
        {
            try
            {
                ble.Connect(bike.deviceId,
                bike.serviceUuid,
                bike.characteristicUuids);
                bike.isConnected = true;
            }
            catch (Exception e)
            {
                Debug.Log("Could not establish connection to device with ID " + deviceId + "\n" + e);
            }
        }

        if (ble.isConnected)
            Debug.Log("Connected to: " + targetDeviceName);
    }
}
