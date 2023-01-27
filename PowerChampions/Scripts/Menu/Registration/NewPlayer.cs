using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NewPlayer : MonoBehaviour
{
    [SerializeField]
    private InputField playerName;
    [SerializeField]
    private InputField playerAge;
    [SerializeField]
    private Dropdown playerGender;
    [SerializeField]
    private InputField playerWeight;
    [SerializeField]
    private Dropdown playerNationality;

    [SerializeField]
    private LeanButton submitButton;

    [SerializeField]
    private LeanPulse leanPulseNotification;
    [SerializeField]
    private Text leanPulseText;
    [SerializeField]
    private Image leanpulseImageColour;

    public void CallAddNewPlayer()
    {
        StartCoroutine("AddNewPlayer");
    }

    private IEnumerator AddNewPlayer()
    {
        WWWForm form = new WWWForm();

        form.AddField("PlayerName", playerName.text);
        form.AddField("PlayerAge", playerAge.text);
        form.AddField("PlayerGender", playerGender.options[playerGender.value].text);
        form.AddField("PlayerWeight", playerWeight.text);
        form.AddField("PlayerNationality", playerNationality.options[playerNationality.value].text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/newplayer.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }

        else
        {

            if (www.downloadHandler.text == "0\r\n")
            {
                //SUCCESS
                leanPulseText.text = "User Created Successfully";
                leanpulseImageColour.color = new Color32(241, 233, 71, 255);
                leanPulseNotification.Pulse();
            }

            else
            {
                //FAIL
                leanPulseText.text = www.downloadHandler.text;
                leanpulseImageColour.color = new Color32(241, 72, 76, 255);
                leanPulseNotification.Pulse();
            }
        }
    }

    public void VerifyInput()
    {
        submitButton.interactable = (playerName.text.Length > 0 && playerName.text.Length <= playerName.characterLimit && playerAge.text.Length > 0 && playerAge.text.Length <= playerAge.characterLimit && playerWeight.text.Length > 0 && playerWeight.text.Length <= playerWeight.characterLimit);
    }
}
