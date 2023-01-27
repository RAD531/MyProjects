using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedRacers : MonoBehaviour
{
    public GameObject racer;
    public float finsihedTime;

    public FinishedRacers(GameObject _racer, float _finshedTime)
    {
        racer = _racer;
        finsihedTime = _finshedTime;
    }
}
