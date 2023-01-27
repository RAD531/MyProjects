using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FetchPlayer : MonoBehaviour
{
    //if loading player
    [SerializeField]
    private Dropdown playerName;

    //if loading player after modification
    [SerializeField]
    private InputField playerNameModify;

    //bool to determine if load or modify
    [SerializeField]
    private bool ModifyingPlayer = false;

    [SerializeField]
    private LeanPulse leanPulseNotification;
    [SerializeField]
    private Text leanPulseText;
    [SerializeField]
    private Image leanpulseImageColour;

    [SerializeField]
    ViewPlayer viewPlayerObject;

    public void CallAddNewPlayer()
    {
        StartCoroutine("ReadPlayer");
    }

    private IEnumerator ReadPlayer()
    {
        WWWForm form = new WWWForm();

        if (ModifyingPlayer)
        {
            form.AddField("PlayerName", playerNameModify.text);
        }

        else
        {
            form.AddField("PlayerName", playerName.options[playerName.value].text);
        }

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/fetchplayer.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }

        else
        {

            if (www.downloadHandler.text == "3: No player found")
            {
                //FAIL
                leanPulseText.text = www.downloadHandler.text;
                leanpulseImageColour.color = new Color32(241, 72, 76, 255);
                leanPulseNotification.Pulse();
            }

            else
            {
                //SUCCESS
                leanPulseText.text = "User Selected";
                leanpulseImageColour.color = new Color32(241, 233, 71, 255);
                leanPulseNotification.Pulse();

                //store results in array
                string[] player = www.downloadHandler.text.Split(',');

                //store array player details in singleton
                for (int i = 0; i < player.Length; i++)
                {
                    player[i].TrimStart();
                }

                try
                {
                    PlayerStaticClass.instance.athleteID = int.Parse(player[0]);
                    PlayerStaticClass.instance.playerID = int.Parse(player[1]);
                    PlayerStaticClass.instance.playerName = player[2];
                    PlayerStaticClass.instance.playerAge = int.Parse(player[3]);
                    PlayerStaticClass.instance.playerWeight = int.Parse(player[4]);
                    PlayerStaticClass.instance.playerNationality = player[5];
                    PlayerStaticClass.instance.playerGender = player[6];
                    PlayerStaticClass.instance.playerLevel = player[7];

                    viewPlayerObject.PopulateViewPlayer();
                }

                catch
                {
                    Debug.LogError("Error converting data to player singleton");
                }
            }
        }
    }
}
