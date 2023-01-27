using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Cinemachine;


/// <summary>
/// This is a very simple behaviour that constrains its transform to a CinemachinePath.  
/// It can be used to animate any objects along a path, or as a Follow target for 
/// Cinemachine Virtual Cameras.
/// </summary>
[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
#if UNITY_2018_3_OR_NEWER
[ExecuteAlways]
#else
    [ExecuteInEditMode]
#endif
[DisallowMultipleComponent]
public class MoveToNextWayPoint : MonoBehaviour
{
    /// <summary>The path to follow</summary>
    [Tooltip("The path to follow")]
    public CinemachinePathBase m_Path;


    /// <summary>How to interpret the Path Position</summary>
    [Tooltip("How to interpret the Path Position.  If set to Path Units, values are as follows: 0 represents the first waypoint on the path, 1 is the second, and so on.  Values in-between are points on the path in between the waypoints.  If set to Distance, then Path Position represents distance along the path.")]
    public CinemachinePathBase.PositionUnits m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;

    /// <summary>Move the cart with this speed</summary>
    [Tooltip("Move the cart with this speed along the path.  The value is interpreted according to the Position Units setting.")]
    [FormerlySerializedAs("m_Velocity")]
    public float m_Speed;

    /// <summary>The cart's current position on the path, in distance units</summary>
    [Tooltip("The position along the path at which the cart will be placed.  This can be animated directly or, if the velocity is non-zero, will be updated automatically.  The value is interpreted according to the Position Units setting.")]
    [FormerlySerializedAs("m_CurrentDistance")]
    public float m_Position;

    private float lastFrameTime = 0f;
    private Vector3 lastFramePos = Vector3.zero;
    private float kmh;

    [SerializeField]
    [Range(2, 45)]
    float angle_range = 45f;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float min_allowed_distance = 1;

    private float angle;

    private void Start()
    {
        lastFramePos = transform.position;
    }


    void FixedUpdate()
    {
        calculateSpeedKMH();
        SetCartPosition(m_Position + m_Speed * Time.deltaTime);
    }

    void SetCartPosition(float distanceAlongPath)
    {
        if (m_Path != null)
        {
            m_Position = m_Path.StandardizeUnit(distanceAlongPath, m_PositionUnits);
            transform.position = m_Path.EvaluatePositionAtUnit(m_Position, m_PositionUnits);
            transform.rotation = m_Path.EvaluateOrientationAtUnit(m_Position, m_PositionUnits);
        }
    }

    private void calculateSpeedKMH()
    {

        Vector3 deltaPos = transform.position - lastFramePos;
        float speedMetersPerSecond = deltaPos.magnitude / (Time.time - lastFrameTime);
        kmh = speedMetersPerSecond * 3.6f;

        lastFramePos = transform.position;
        lastFrameTime = Time.time;
    }

    public void syncSpeeds(float playerKMH, Vector3 playerPos)
    {
        if (kmh > playerKMH)
        {
            m_Speed--;
        }

        if (isWithinDistanceToPlayer(playerPos))
        {
            m_Speed = 30;
            m_Speed++;
        }

        else
        {
            if (m_Speed > 0)
            {
                m_Speed--;
            }

            else
            {
                m_Speed = 0;
            }
        }
    }

    private bool isWithinDistanceToPlayer(Vector3 playerPos)
    {
        angle = Vector3.Angle(this.transform.forward, playerPos - this.transform.position);

        if (Mathf.Abs(angle) > angle_range)
        {
            if (Vector3.Distance(this.transform.position, playerPos) > min_allowed_distance)
            {
                return false;
            }
        }

        return true;
    }
}
