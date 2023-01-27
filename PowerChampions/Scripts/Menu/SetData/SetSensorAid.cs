using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSensorAid : MonoBehaviour
{
    [SerializeField]
    private Slider sensorAidSlider;
    [SerializeField]
    private Image sliderColour;
    [SerializeField]
    private Text aidValueLabel;
    [SerializeField]
    private Text aidValueDifficulty;

    public void UpdateSensorAid()
    {
        PlayerStaticClass.instance.sensorAid = (int)sensorAidSlider.value;
        aidValueLabel.text = sensorAidSlider.value.ToString() + "+ Extra kh/h";

        if(sensorAidSlider.value >= 16)
        {
            sliderColour.color = new Color32(136, 255, 143, 255);
            aidValueDifficulty.text = "Difficulty = Easy";
        }

        else if (sensorAidSlider.value > 5 && sensorAidSlider.value < 16)
        {
            sliderColour.color = new Color32(255, 223, 123, 255);
            aidValueDifficulty.text = "Difficulty = Medium";
        }
        else
        {
            sliderColour.color = new Color32(255, 136, 142, 255);
            aidValueDifficulty.text = "Difficulty = Extreme";
        }
    }
}
