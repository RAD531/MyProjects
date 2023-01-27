using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FetchNations : MonoBehaviour
{
    [SerializeField]
    private Dropdown playerNationality;

    private void Start()
    {
        StartCoroutine("RetrieveData");
    }

    public IEnumerator RetrieveData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/sqlconnect/fetchnation.php"))
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
                    PopulateNationDropdown(webRequest);
                    break;
            }
        }
    }

    private void PopulateNationDropdown(UnityWebRequest result)
    {
        string[] nations = result.downloadHandler.text.Split(',');

        foreach(string nation in nations)
        {
            string trimmedNation = nation.TrimStart();
            playerNationality.options.Add(new Dropdown.OptionData(trimmedNation));
        }

        playerNationality.captionText.text = playerNationality.options[playerNationality.value].text;
    }
}
