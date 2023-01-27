using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSensorProtocolSingleton : MonoBehaviour
{
    #region Singleton 

    public static SpeedSensorProtocolSingleton instance;
    public List<BikeDisplayNew> bikeDisplayList = new List<BikeDisplayNew>();
    public LeanToggle leanToggle;

    private List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
    private Dropdown.OptionData item = new Dropdown.OptionData();
    private BikeDisplayNew[] chosenBikeDisplayList = new BikeDisplayNew[2];

    //==========
    //Ant+
    //==========
    AntPlusSpeed antPlus = new AntPlusSpeed();

    //==========
    //BLE
    //==========
    [SerializeField]
    private BLESpeed ble;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    private void Start()
    {
        try
        {
            //ant+
            AntManager.Instance.Init();
            antPlus.StartScan();
        }

        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        try
        {
            //ble
            fillOptionsBLE();
            ble.StartScan();
        }

        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void fillOptionsBLE()
    {
        bikeDisplayList = ble.bikeDisplayList;

        options.Clear();
        item.text = "select sensor";
        options.Add(item);

        //fill the options data, start at 1 so that Unity Event picks up value changed
        for (int o = 0; o < ble.bikeDisplayList.Count; o++)
        {
            //need a new object as "item" is still be referenced in the options[0]
            Dropdown.OptionData newItem = new Dropdown.OptionData();
            newItem.text = ble.bikeDisplayList[o].deviceId.ToString();
            options.Add(newItem);
        }
    }

    public void fillOptionsAnt()
    {
        bikeDisplayList = antPlus.bikeDisplayList;

        options.Clear();
        item.text = "select sensor";
        options.Add(item);

        //fill the options data, start at 1 so that Unity Event picks up value changed
        for (int o = 0; o < antPlus.bikeDisplayList.Count; o++)
        {
            //need a new object as "item" is still be referenced in the options[0]
            Dropdown.OptionData newItem = new Dropdown.OptionData();
            newItem.text = antPlus.bikeDisplayList[o].deviceNumber.ToString();
            options.Add(newItem);
        }
    }

    public Dropdown[] FillDropdownSensorData(Dropdown[] d)
    {

        for (int bikeIndex = 0; bikeIndex < chosenBikeDisplayList.Length; bikeIndex++)
        {
            //if it has a value in
            if (d[bikeIndex].value != 0)
            {
                //Check which protocol we are searching for and whether a selection has already been 
                //made on the other protocol

                if (leanToggle.On && chosenBikeDisplayList[bikeIndex].GetProtocol() == BIKEPROTOCOL.BLE || !leanToggle.On && chosenBikeDisplayList[bikeIndex].GetProtocol() == BIKEPROTOCOL.ANTPLUS)
                {
                    //make the dropdown unaccessible
                    d[bikeIndex].interactable = false;
                    //dont do anything else with dropdown
                    continue;
                }

                else
                {
                    //make the dropdown accessible
                    d[bikeIndex].interactable = true;
                }
            }


            if (options == null || options.Count == 0)
            {
                return d;
            }

            d[bikeIndex].options.Clear();

            foreach (Dropdown.OptionData o in options)
            {
                d[bikeIndex].options.Add(o);
            }

        }

        return d;
    }

    public void UpdateChosenBikeSensors(BikeDisplayNew sensor, int wheelPos)
    {
        if (wheelPos == 0)
        {
            chosenBikeDisplayList[0] = sensor;
        }

        else if (wheelPos == 1)
        {
            chosenBikeDisplayList[1] = sensor;
        }

        else
        {
            Debug.LogError("Attempting to assign an invalid wheel pos, must be either left(0) or right(1)");
        }
    }

    public BikeDisplayNew RetreiveChosenBikeSensors(int wheelPos)
    {
        if (wheelPos > 2)
        {
            Debug.LogError("Attempting to retreive an invalid wheel pos, must be either left(1) or right(2)");
            return null;
        }

        return chosenBikeDisplayList[wheelPos];
    }

    public void SetBike(int id)
    {

        if (leanToggle.On)
        {
            bikeDisplayList = antPlus.bikeDisplayList;
        }

        else
        {
            bikeDisplayList = ble.bikeDisplayList;
        }


        //equals select sensor option, can presume user wants to disconnect
        if (id < 1)
        {
            //bikeDisplayList[id].isConnected = false;
            return;
        }
       

        //already connected
        if (bikeDisplayList[id - 1].isConnected == true)
        {
            return;
        }

        //try to connect to either ble or ant+
        try
        {
            //Ant+
            if (leanToggle.On)
            {
                antPlus.ConnectTospeed(bikeDisplayList[id - 1]);
            }

            //BLE
            else
            {
                ble.StartConHandler(bikeDisplayList[id - 1]);
            }
        }

        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return;
        }

        //can now set device to connected
        bikeDisplayList[id - 1].isConnected = true;

    }
}
