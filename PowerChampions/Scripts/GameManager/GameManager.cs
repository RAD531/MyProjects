using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton 

    public static GameManager instance;
    public enum RaceCondition
    {
        START,
        RUNNING,
        END
    };

    public enum RaceType
    {
        ONEHUNDRED,
        TWOHUNDRED,
        THREEHUNDRED
    };

    public RaceCondition raceCondition;
    public RaceType raceType;

    public List<FinishedRacers> finishedLeaderboard = new List<FinishedRacers>();

    //Populate the leaderboard with the players who finished
    [SerializeField]
    private EFE_Base efeBase;
    [SerializeField]
    private GameObject finishedLeaderboardPanel;
    [SerializeField]
    private GameObject[] leaderboardPos;


    //Player Objects
    [SerializeField]
    private GameObject playerWheelchairRacer;

    [SerializeField]
    private Transform playerPosition;
    [SerializeField]
    private Transform playerTargetToFollow;

    //AI Objects
    [SerializeField]
    private GameObject AIwheelchairRacer;

    [SerializeField]
    List<Transform> opponentsPositions = new List<Transform>();
    [SerializeField]
    List<Transform> targetsToFollow = new List<Transform>();

    private void Awake()
    {
        instance = this;
        raceCondition = RaceCondition.START;
        raceType = RaceType.ONEHUNDRED;
        finishedLeaderboard.Clear();
    }

    #endregion

    private void Start()
    {
        //hide the leaderbaord panel until player finishes
        efeBase.ClosePanel(finishedLeaderboardPanel);


        InstantiateOpponents();
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        playerWheelchairRacer.name = PlayerStaticClass.instance.playerName;
        playerWheelchairRacer.transform.position = playerPosition.position;
        playerWheelchairRacer.transform.rotation = playerPosition.rotation;
        playerWheelchairRacer.GetComponent<PlayerController>().targetToFollow = playerTargetToFollow;
        playerWheelchairRacer.tag = "Player";
        playerTargetToFollow.GetComponent<MeshRenderer>().forceRenderingOff = true;

        Instantiate(playerWheelchairRacer);
    }

    private void InstantiateOpponents()
    {
        for (int i = 0; i < AIStaticClass.instance.opponents.Count; i++)
        {
            AIwheelchairRacer.GetComponent<Abilities>().athleteID = AIStaticClass.instance.opponents[i].athleteID;
            AIwheelchairRacer.GetComponent<Abilities>().athleteName = AIStaticClass.instance.opponents[i].athleteName;
            AIwheelchairRacer.GetComponent<Abilities>().age = AIStaticClass.instance.opponents[i].age;
            AIwheelchairRacer.GetComponent<Abilities>().armPower = AIStaticClass.instance.opponents[i].armPower;
            AIwheelchairRacer.GetComponent<Abilities>().gender = AIStaticClass.instance.opponents[i].gender;
            AIwheelchairRacer.GetComponent<Abilities>().maxSpeed = AIStaticClass.instance.opponents[i].maxSpeed;
            AIwheelchairRacer.GetComponent<Abilities>().nationality = AIStaticClass.instance.opponents[i].nationality;
            AIwheelchairRacer.GetComponent<Abilities>().stamina = AIStaticClass.instance.opponents[i].stamina;
            AIwheelchairRacer.GetComponent<Abilities>().startResponseTime = AIStaticClass.instance.opponents[i].startResponseTime;
            AIwheelchairRacer.GetComponent<Abilities>().weight = AIStaticClass.instance.opponents[i].weight;
            AIwheelchairRacer.GetComponent<Abilities>().willingness = AIStaticClass.instance.opponents[i].willingness;

            AIwheelchairRacer.name = AIStaticClass.instance.opponents[i].athleteName.ToString();
            AIwheelchairRacer.transform.position = opponentsPositions[i].transform.position;
            AIwheelchairRacer.transform.rotation = opponentsPositions[i].transform.rotation;

            targetsToFollow[i].GetComponent<MeshRenderer>().forceRenderingOff = true;
            AIwheelchairRacer.GetComponent<AIController>().targetToFollow = targetsToFollow[i];

            Instantiate(AIwheelchairRacer);
        }
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < finishedLeaderboard.Count; i++)
        {
            //check if leaderboard pos is already active, if it is, then we dont need to mess with it or update values
            if (leaderboardPos[i].activeSelf == false)
            {
                //set the leaderboard pos to true
                leaderboardPos[i].SetActive(true);

                //populate the leaderboard depending on whether it is player or AI
                leaderboardPos[i].transform.Find("position").gameObject.GetComponent<Text>().text = "Position: " + i.ToString();
                leaderboardPos[i].transform.Find("finishTime").gameObject.GetComponent<Text>().text = "Finish Time: " + finishedLeaderboard[i].finsihedTime.ToString();

                if (finishedLeaderboard[i].racer.tag == "Opponent")
                {
                    leaderboardPos[i].transform.Find("maxSpeed").gameObject.GetComponent<Text>().text = "Max Speed: " + (Mathf.Round(finishedLeaderboard[i].racer.GetComponent<Abilities>().gameMaxSpeed * 100f) / 100f).ToString() + "km/h";

                    leaderboardPos[i].transform.Find("playerName").gameObject.GetComponent<Text>().text = "Racer Name: " + finishedLeaderboard[i].racer.GetComponent<Abilities>().athleteName.ToString();
                }


                if (finishedLeaderboard[i].racer.tag == "Player")
                {

                    leaderboardPos[i].transform.Find("maxSpeed").gameObject.GetComponent<Text>().text = "Max Speed: " + (Mathf.Round(PlayerStaticClass.instance.gameMaxSpeed * 100f) / 100f).ToString() + "km/h";
                    leaderboardPos[i].transform.Find("playerName").gameObject.GetComponent<Text>().text = "Player Name: " + PlayerStaticClass.instance.playerName.ToString();

                    //show finsihed panel
                    efeBase.OpenOverlayPanel(finishedLeaderboardPanel);
                }
            }
        }
    }
}
