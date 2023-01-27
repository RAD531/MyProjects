using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    //opponent attributes
    public int athleteID;
    public string athleteName;
    public int age;
    public string nationality;
    public string gender;
    public int weight;
    public string level;
    public float maxSpeed;
    public float startResponseTime;
    public float armPower;
    public float stamina;
    public string willingness;

    public float endurance = 100;
    private float staticMaxSpeed;

    //store the maxSpeed the player has gone at in the last race
    public float gameMaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (willingness == "Poor")
        {
            maxSpeed = maxSpeed - 6;
        }

        else if (willingness == "Average")
        {
            maxSpeed = maxSpeed - 3;
        }

        else if (willingness == "Very_Good")
        {
            maxSpeed = maxSpeed - 2;
        }

        staticMaxSpeed = maxSpeed;
    }

    private void FixedUpdate()
    {
        if (endurance <= 0)
        {
            if (maxSpeed > staticMaxSpeed - 5)
            {
                maxSpeed = maxSpeed - 5;
            }
        }

        else if (endurance >= 100)
        {
            maxSpeed = staticMaxSpeed;
        }

        //update the maxSpeed that the AI has gone at - only if slower than current speed
        if (gameMaxSpeed < maxSpeed)
        {
            gameMaxSpeed = maxSpeed;
        }
    }
}
