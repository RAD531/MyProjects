using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineTarget : MonoBehaviour
{
    [SerializeField]
    private string targetTag;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<CinemachineVirtualCamera>().LookAt = GameObject.FindGameObjectWithTag(targetTag).transform;
        this.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag(targetTag).transform;
    }
}
