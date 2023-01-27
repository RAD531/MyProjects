using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FetchOpponents : MonoBehaviour
{
    [SerializeField]
    private LeanPulse leanPulseNotification;
    [SerializeField]
    private Text leanPulseText;
    [SerializeField]
    private Image leanpulseImageColour;

    [SerializeField]
    private string gameSceneToBeLoaded;

    [SerializeField]
    private ValidatePlayer validatePlayer;


    public void CallRetrieveOppenants()
    {
        if (validatePlayer.playerChecked)
        {
            StartCoroutine("RetrieveData");
        }
    }

    private IEnumerator RetrieveData()
    {
        string level = PlayerStaticClass.instance.playerLevel;
        level = level.TrimEnd('\r', '\n');

        WWWForm form = new WWWForm();
        form.AddField("PlayerLevel", level);


        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/sqlconnect/fetchopponents.php", form))
        {
            //Reqest and wait for response
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("Connection Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Data Proccesing Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    PopulateOpponents(webRequest);
                    break;
            }
        }
    }

    private void PopulateOpponents(UnityWebRequest result)
    {
        try
        {
            //clear the opponents static first before adding again
            AIStaticClass.instance.opponents.Clear();

            string resultText = result.downloadHandler.text;

            resultText = resultText.TrimEnd('\r', '\n');

            resultText = resultText.TrimEnd(' ');

            //split the opponents by a text code
            string[] opponents = resultText.Split('#');

            foreach (string opponent in opponents)
            {
                //now split their abilities
                string[] opponentsAbilities = opponent.Split(',');

                if (opponentsAbilities[0] == "")
                {
                    //we dont want the last empty value
                    break;
                }

                //string abilitString = ability.TrimStart();

                /*if (AIStaticClass.instance.opponents != null)
                {
                    //clear the list before populating
                    AIStaticClass.instance.opponents.Clear();
                }*/

                //add the abilities to the static class to use within the game
                Opponent opponent1 = new Opponent();
                opponent1.athleteID = int.Parse(opponentsAbilities[0]);
                opponent1.athleteName = opponentsAbilities[1].ToString();
                opponent1.age = int.Parse(opponentsAbilities[2]);
                opponent1.nationality = opponentsAbilities[3].ToString();
                opponent1.gender = opponentsAbilities[4].ToString();
                opponent1.weight = int.Parse(opponentsAbilities[5]);
                opponent1.level = opponentsAbilities[6].ToString();
                opponent1.maxSpeed = int.Parse(opponentsAbilities[7]);
                opponent1.startResponseTime = int.Parse(opponentsAbilities[8]);
                opponent1.armPower = int.Parse(opponentsAbilities[9]);
                opponent1.stamina = int.Parse(opponentsAbilities[10]);
                opponent1.willingness = opponentsAbilities[11].ToString();

                //add the oppenant to the static class
                AIStaticClass.instance.opponents.Add(opponent1);
            }

            //if all succeeds, load the stadium
            SceneManager.LoadScene(gameSceneToBeLoaded); 
        }

        catch (Exception e)
        {
            //FAIL
            leanPulseText.text = "Error Populating Opponants";
            Debug.Log(e);
            AIStaticClass.instance.opponents.Clear();
            leanpulseImageColour.color = new Color32(241, 72, 76, 255);
            leanPulseNotification.Pulse();
        }

    }
}
