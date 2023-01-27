using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FetchPlayers : MonoBehaviour
{
    [SerializeField]
    private Dropdown playerList;

    private void Start()
    {
        CallRetrievePlayers();
    }

    public void CallRetrievePlayers()
    {
        StartCoroutine("RetrieveData");
    }

    private IEnumerator RetrieveData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/sqlconnect/fetchplayers.php"))
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
                    PopulatePlayerDropdown(webRequest);
                    break;
            }
        }
    }

    private void PopulatePlayerDropdown(UnityWebRequest result)
    {
        playerList.ClearOptions();

        string[] players = result.downloadHandler.text.Split(',');

        foreach (string player in players)
        {
            string trimmedNation = player.Trim();
            playerList.options.Add(new Dropdown.OptionData(trimmedNation));
        }

        playerList.captionText.text = playerList.options[playerList.value].text;
    }
}
