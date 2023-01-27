using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ValidatePlayer : MonoBehaviour
{
    [SerializeField]
    private LeanPulse leanPulseNotification;
    [SerializeField]
    private Text leanPulseText;
    [SerializeField]
    private Image leanpulseImageColour;

    private bool hasCheckedSensorAid = false;

    public bool playerChecked = false;

    public void checkPlayer()
    {
        if (SpeedSensorProtocolSingleton.instance.RetreiveChosenBikeSensors(0) == null || SpeedSensorProtocolSingleton.instance.RetreiveChosenBikeSensors(1) == null)
        {
            //FAIL
            leanPulseText.text = "Please select two sensors to start a race";
            leanpulseImageColour.color = new Color32(241, 72, 76, 255);
            leanPulseNotification.Pulse();
        }

        else if (PlayerStaticClass.instance.athleteID == 0)
        {
            //FAIL
            leanPulseText.text = "No player selected, please create or load a player";
            leanpulseImageColour.color = new Color32(241, 72, 76, 255);
            leanPulseNotification.Pulse();
        }

        //checking if the sensor aid has been changed from first start
        else if (PlayerStaticClass.instance.sensorAid == -1)
        {
            //FAIL
            leanPulseText.text = "Please review the sensor aid settings";
            leanpulseImageColour.color = new Color32(241, 72, 76, 255);
            leanPulseNotification.Pulse();
        }

        else
        {
            playerChecked = true;
        }
    }

    public void CheckedSensorAid()
    {
        hasCheckedSensorAid = true;

        if(PlayerStaticClass.instance.sensorAid == -1)
        {
            PlayerStaticClass.instance.sensorAid = 30;
        }
    }


}
