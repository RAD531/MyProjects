using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPlayer : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        PopulateViewPlayer();   
    }

    public void PopulateViewPlayer()
    {
        if(PlayerStaticClass.instance.playerName != "")
        {
            playerName.text = PlayerStaticClass.instance.playerName;
            playerAge.text = PlayerStaticClass.instance.playerAge.ToString();
            playerGender.value = playerGender.options.FindIndex(option => option.text.IndexOf(PlayerStaticClass.instance.playerGender) >=0 ); 
            playerWeight.text = PlayerStaticClass.instance.playerWeight.ToString();
            playerNationality.value = playerNationality.options.FindIndex(option => option.text.IndexOf(PlayerStaticClass.instance.playerNationality) >= 0);
        }
    }
}
