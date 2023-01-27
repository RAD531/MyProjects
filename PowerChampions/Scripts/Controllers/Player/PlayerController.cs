using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public WheelCollider BackleftWheelCollider;
    public WheelCollider BackrightWheelCollider;
    public WheelCollider FrontWheelCollider;
    public Transform BackleftWheelT;
    public Transform BackrightWheelT;
    public Transform FrontWheelT;

    public int brake;

    private float leftWheelInput;
    private float rightWheelInput;
    private new Rigidbody rigidbody;

    //private InputMaster controls;
    private bool sensorsFound = false;

    private Animator playerAnimator;

    public Transform targetToFollow;
    private Vector3 chairToTarget;
    private float dirNum;

    private void Awake()
    {
        //controls = new InputMaster();

        //controls.Player.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        //controls.Player.Movement.canceled += ctx => move = Vector2.zero;

        //controls.Player.Push.canceled += ctx => CancelPush();

    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = new Vector3(rigidbody.centerOfMass.x, rigidbody.centerOfMass.y - 1, rigidbody.centerOfMass.z);

        playerAnimator = GetComponent<Animator>();
    }

    private void GetInput()
    {
        try
        {
            //leftWheelInput = GameObject.FindGameObjectWithTag("MultipleSpeedDisplay").GetComponent<MultiBikeSpeedDisplayExample>().bikeDisplayList[0].speed;
            //rightWheelInput = GameObject.FindGameObjectWithTag("MultipleSpeedDisplay").GetComponent<MultiBikeSpeedDisplayExample>().bikeDisplayList[1].speed;

            leftWheelInput = PlayerStaticClass.instance.leftWheelSpeed;
            rightWheelInput = PlayerStaticClass.instance.rightWheelSpeed;
            sensorsFound = true;
        }

        catch
        {
            sensorsFound = false;
            Debug.Log("No sensors found just yet");
        }

    }


    private IEnumerator GetRightWheelSpeed()
    {
        if (rightWheelInput == 0)
        {
            //Wait for 1 second
            yield return new WaitForSeconds(1);

            Debug.ClearDeveloperConsole();

            rightWheelInput = PlayerStaticClass.instance.rightWheelSpeed;
        }
    }

    private IEnumerator GetLeftWheelSpeed()
    {
        if (leftWheelInput == 0)
        {
            //Wait for 1 second
            yield return new WaitForSeconds(1);

            leftWheelInput = PlayerStaticClass.instance.leftWheelSpeed;
        }
    }

    private void Difficulty()
    {
        if (rightWheelInput > 0 || leftWheelInput > 0)
        {
            rightWheelInput = rightWheelInput + PlayerStaticClass.instance.sensorAid;
            leftWheelInput = leftWheelInput + PlayerStaticClass.instance.sensorAid;
        }
    }

    private void Accelerate()
    {

        /*if (rightWheelInput == 0)
        {
            if (rightCounter < 3)
            {
                rightWheelInput = oldrightWheelInput;
            }

            rightCounter++;
        }

        else
        {
            oldrightWheelInput = rightWheelInput;
            rightCounter = 0;
        }

        if (leftWheelInput == 0)
        {
            if (leftCounter < 3)
            {
                leftWheelInput = oldleftWheelInput;
            }

            leftCounter++;
        }

        else
        {
            oldleftWheelInput = leftWheelInput;
            leftCounter = 0;
        }*/


        /*if (rightWheelInput > 0.4 && leftWheelInput > 0.4)
        {
            rightWheelInput = (rightWheelInput + leftWheelInput) / 2;
            leftWheelInput = rightWheelInput;
        }*/

        //km/h
        //rightWheelInput = 30f;
        //leftWheelInput = 30f;

        //rightWheelInput = Input.GetAxis("Vertical") * 22;
        //leftWheelInput = rightWheelInput;

        //BackleftWheelCollider.steerAngle = 0;
        //BackrightWheelCollider.steerAngle = 0;
        //FrontWheelCollider.steerAngle = Input.GetAxis("Horizontal") * 3;

        Debug.Log("right wheel input: " + rightWheelInput);
        Debug.Log("left wheel input: " + leftWheelInput);

        rightWheelInput = (rightWheelInput + leftWheelInput) / 2;
        leftWheelInput = rightWheelInput;


        //mps to km/h
        float speed = this.rigidbody.velocity.magnitude * 3.6f;

        if (speed > 2)
        {
            GameManager.instance.raceCondition = GameManager.RaceCondition.RUNNING;
        }

        if (targetToFollow != null)
        {
            //this.transform.LookAt(targetToFollow.transform.position);

           // FrontWheelCollider.transform.LookAt(targetToFollow.transform.position);

            chairToTarget = targetToFollow.position - FrontWheelCollider.transform.position;
            dirNum = AngleDir(FrontWheelCollider.transform.forward, chairToTarget, FrontWheelCollider.transform.up);


            
            if (dirNum > 0)
            {
                FrontWheelCollider.steerAngle = 1;
                //BackleftWheelCollider.steerAngle = 1;
                //BackrightWheelCollider.steerAngle = 1;
                //Debug.Log("Angle: " + dirNum + " :::::::::: steerAngle: " + FrontWheelCollider.steerAngle);
            }

            else if (dirNum < 0)
            {
                FrontWheelCollider.steerAngle = -4;
                //BackleftWheelCollider.steerAngle = -4;
                //BackrightWheelCollider.steerAngle = -4;
            }

            Debug.Log("Angle: " + dirNum + " :::::::::: steerAngle: " + FrontWheelCollider.steerAngle);


            targetToFollow.GetComponent<MoveToNextWayPoint>().syncSpeeds(speed, this.transform.position);
        }

        if (speed < rightWheelInput)
        {
            BackrightWheelCollider.motorTorque += Time.deltaTime * 2;
            BackleftWheelCollider.motorTorque += Time.deltaTime * 2;
            FrontWheelCollider.motorTorque += Time.deltaTime * 2;
        }

        else
        {
            if (BackrightWheelCollider.motorTorque <= 0 || BackleftWheelCollider.motorTorque <= 0)
            {
                BackrightWheelCollider.motorTorque = 0;
                BackleftWheelCollider.motorTorque = 0;
                FrontWheelCollider.motorTorque = 0;
            }

            else
            {
                BackrightWheelCollider.motorTorque -= Time.deltaTime * 2;
                BackleftWheelCollider.motorTorque -= Time.deltaTime * 2;
                FrontWheelCollider.motorTorque -= Time.deltaTime * 2;
            }
        }

        //Debug.Log("Left Wheel Input: " + leftWheelInput.ToString() + " :::: Left Wheel Actual Speed: " + speed.ToString() + " :::: Left Wheel RPM " + BackleftWheelCollider.rpm.ToString());

        //Debug.Log("Right Wheel Input: " + rightWheelInput.ToString() + " :::: Right Wheel Actual Speed: " + speed.ToString() + " :::: Right Wheel RPM " + BackrightWheelCollider.rpm.ToString());


        /*if (rightWheelInput <= 0)
        {
            BackrightWheelCollider.brakeTorque = 3;
        }

        else
        {
            BackrightWheelCollider.brakeTorque = 0;
        }

        if (leftWheelInput <= 0)
        {
            BackleftWheelCollider.brakeTorque = 3;
        }

        else
        {
            BackleftWheelCollider.brakeTorque = 0;
        }*/


        //trigger animator movement when either wheelcollider is moving
        if (BackleftWheelCollider.motorTorque > 0 || BackrightWheelCollider.motorTorque > 0)
        {
            playerAnimator.SetBool("IsPushing", true);
        }

        else
        {
            playerAnimator.SetBool("IsPushing", false);
        }

        //update the maxSpeed that the player has gone at - only if slower than current speed
        if (PlayerStaticClass.instance.gameMaxSpeed < speed)
        {
            PlayerStaticClass.instance.gameMaxSpeed = speed;
        }

    }
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(BackleftWheelCollider, BackleftWheelT);
        UpdateWheelPose(BackrightWheelCollider, BackrightWheelT);

        UpdateWheelPose(FrontWheelCollider, FrontWheelT);
        //UpdateWheelPose(FrontrightWheelCollider, FrontrightWheelT);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;


        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }

    private void RaceConditions()
    {
        //we dont want the athletes to steer before finish line
        if (GameManager.instance.raceType == GameManager.RaceType.ONEHUNDRED)
        {
            foreach (FinishedRacers racer in GameManager.instance.finishedLeaderboard)
            {
                if (racer.racer.Equals(this.gameObject))
                {
                    rigidbody.constraints = RigidbodyConstraints.None;
                    rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                    return;
                }
            }

            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    private void FixedUpdate()
    {
        GetInput();

        if (sensorsFound)
        {
            StartCoroutine("GetRightWheelSpeed");
            StartCoroutine("GetLeftWheelSpeed");
            Difficulty();
            Accelerate();
            UpdateWheelPoses();
            RaceConditions();
        }

        //Accelerate();
        //UpdateWheelPoses();

    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        
        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }
}