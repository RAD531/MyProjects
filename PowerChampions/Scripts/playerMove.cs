using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{

    float moveSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = GameObject.Find("SpeedDisplay").GetComponent<SpeedDisplay>().speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveSpeed = GameObject.Find("SpeedDisplay").GetComponent<SpeedDisplay>().speed;
        playerMovement();
    }

    void playerMovement()
    {
        transform.Translate(new Vector3(0, 0, 1) * (moveSpeed * Time.deltaTime));
    }
}
