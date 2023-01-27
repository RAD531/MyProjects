using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class FinishLineTrigger : MonoBehaviour
{

    private Stopwatch stopwatch;

    private void Start()
    {
        stopwatch = new Stopwatch();
    }

    private void FixedUpdate()
    {
        //wait until race has started
        if (GameManager.instance.raceCondition == GameManager.RaceCondition.START)
        {
            stopwatch.Start();
        }

        else if (GameManager.instance.raceCondition == GameManager.RaceCondition.END)
        {
            stopwatch.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Opponent")
        {

            foreach(FinishedRacers racer in GameManager.instance.finishedLeaderboard)
            {
                if (racer.racer.gameObject.Equals(other.gameObject))
                {
                    return;
                }
            }

            GameManager.instance.finishedLeaderboard.Add(new FinishedRacers(other.gameObject, stopwatch.ElapsedMilliseconds));
        }
    }
}
