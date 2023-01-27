using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaticClass : MonoBehaviour
{
    #region Singleton 

    public static PlayerStaticClass instance;

    [HideInInspector]
    public float leftWheelSpeed;
    [HideInInspector]
    public float rightWheelSpeed;
    [HideInInspector]
    public int sensorAid;

    //player attributes
    [HideInInspector]
    public int athleteID;
    [HideInInspector]
    public int playerID;
    [HideInInspector]
    public string playerName;
    [HideInInspector]
    public int playerAge;
    [HideInInspector]
    public string playerNationality;
    [HideInInspector]
    public string playerGender;
    [HideInInspector]
    public int playerWeight;
    [HideInInspector]
    public string playerLevel;

    [HideInInspector]
    //store the maxSpeed the player has gone at in the last race
    public float gameMaxSpeed;


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

    // Start is called before the first frame update
    void Start()
    {
        this.sensorAid = -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetWheelSpeed();
    }

    void GetWheelSpeed()
    {
        try
        {
            BikeDisplayNew leftWheelSensor = SpeedSensorProtocolSingleton.instance.RetreiveChosenBikeSensors(1);
            leftWheelSpeed = leftWheelSensor.speed;

            BikeDisplayNew rightWheelSensor = SpeedSensorProtocolSingleton.instance.RetreiveChosenBikeSensors(2);
            rightWheelSpeed = rightWheelSensor.speed;
        }

        catch
        {
            //Debug.Log("No sensor speed found just yet");
        }
    }
}
