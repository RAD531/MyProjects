using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public WheelCollider BackleftWheelCollider;
    public WheelCollider BackrightWheelCollider;
    public WheelCollider FrontWheelCollider;
    public Transform BackleftWheelT;
    public Transform BackrightWheelT;
    public Transform FrontWheelT;

    public int brake;

    private new Rigidbody rigidbody;


    private Animator playerAnimator;

    public Transform targetToFollow;
    private Vector3 chairToTarget;
    private float dirNum;

    private bool isIncreasingStam = false;
    private bool decreaseStam = true;
    private bool increaseStam = true;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = new Vector3(rigidbody.centerOfMass.x, rigidbody.centerOfMass.y - 1, rigidbody.centerOfMass.z);

        playerAnimator = GetComponent<Animator>();
    }

    /*private void OnEnable()
    {
        BackleftWheelCollider.ConfigureVehicleSubsteps(5, 12, 15);
        BackrightWheelCollider.ConfigureVehicleSubsteps(5, 12, 15);
        FrontWheelCollider.ConfigureVehicleSubsteps(5, 12, 15);
    }*/

    private void Accelerate()
    {
        //mps to km/h
        float speed = this.rigidbody.velocity.magnitude * 3.6f;

        //Debug.Log(speed);

        if (targetToFollow != null)
        {

            chairToTarget = targetToFollow.position - FrontWheelCollider.transform.position;
            dirNum = AngleDir(FrontWheelCollider.transform.forward, chairToTarget, FrontWheelCollider.transform.up);

            if (dirNum > 0)
            {
                FrontWheelCollider.steerAngle = 1;
            }

            else if (dirNum < 0)
            {
                FrontWheelCollider.steerAngle = -4;
            }

            targetToFollow.GetComponent<MoveToNextWayPoint>().syncSpeeds(speed, this.transform.position);
        }

        if (speed < this.GetComponent<Abilities>().maxSpeed)
        {
            BackrightWheelCollider.motorTorque += Time.deltaTime * this.GetComponent<Abilities>().armPower;
            BackleftWheelCollider.motorTorque += Time.deltaTime * this.GetComponent<Abilities>().armPower;
            FrontWheelCollider.motorTorque += Time.deltaTime * this.GetComponent<Abilities>().armPower;
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
                BackrightWheelCollider.motorTorque -= Time.deltaTime * this.GetComponent<Abilities>().armPower;
                BackleftWheelCollider.motorTorque -= Time.deltaTime * this.GetComponent<Abilities>().armPower;
                FrontWheelCollider.motorTorque -= Time.deltaTime * this.GetComponent<Abilities>().armPower;
            }

            StartCoroutine("ReduceEndurance");
        }

        //trigger animator movement when either wheelcollider is moving
        if (BackleftWheelCollider.motorTorque > 0 || BackrightWheelCollider.motorTorque > 0)
        {
            playerAnimator.SetBool("IsPushing", true);
        }

        else
        {
            playerAnimator.SetBool("IsPushing", false);
        }

    }
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(BackleftWheelCollider, BackleftWheelT);
        UpdateWheelPose(BackrightWheelCollider, BackrightWheelT);

        UpdateWheelPose(FrontWheelCollider, FrontWheelT);
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
                if (racer.racer.gameObject.Equals(this.gameObject))
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
        if (GameManager.instance.raceCondition == GameManager.RaceCondition.RUNNING)
        {
            Accelerate();
            UpdateWheelPoses();
            RaceConditions();
        }
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

    private IEnumerator ReduceEndurance()
    {
        if (this.GetComponent<Abilities>().endurance > 0)
        {
            if (!isIncreasingStam)
            {
                if (decreaseStam == true)
                {
                    decreaseStam = false;

                    //Wait for x seconds
                    yield return new WaitForSeconds(this.GetComponent<Abilities>().stamina / 10);

                    this.GetComponent<Abilities>().endurance -= 10;

                    decreaseStam = true;
                }
            }

            else
            {
                StartCoroutine("IncreaseEndurance");
            }
        }

        else
        {
            StartCoroutine("IncreaseEndurance");
        }
    }

    private IEnumerator IncreaseEndurance()
    {
        if (increaseStam == true)
        {
            isIncreasingStam = true;

            decreaseStam = false;
            increaseStam = false;

            //recovery time
            yield return new WaitForSeconds(0.5f);

            //recovery rate
            this.GetComponent<Abilities>().endurance += 10;

            if (this.GetComponent<Abilities>().endurance > 100)
            {
                isIncreasingStam = false;

                decreaseStam = true;

                this.GetComponent<Abilities>().endurance = 100;
            }

            increaseStam = true;
        }
    }
}
