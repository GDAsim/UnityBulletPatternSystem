using UnityEngine;

public class HomingShootSystemController : MonoBehaviour
{
    [SerializeField] ShootSystemData BaseShootSystemStats;
    [SerializeField] HomingType homingType;

    [SerializeField] ShootSystem Gun;

    [Header("Shoot Properties")]
    [SerializeField] float ShootPower = 2;

    [Header("Homing Properties")]
    [SerializeField] Transform homingTarget;
    [SerializeField] float HomingRate = 15f; // Homing In Degrees per second

    [Header("Distance Proximity Properties")]
    [SerializeField] float ProximityDistance = 10; // Radius from the target to start homing

    [Header("Limited Proximity Properties")]
    [SerializeField] float LimitedProximityFactor = 1.1f; // Radius from the target to start homing

    [Header("Accelerated Properties")]
    [SerializeField] float AcceleratedRadius = 10; // Radious from the target to start accelerating
    [SerializeField] float AccelerationMulti = 3; // Max Acceleration Multiplier

    enum HomingType { Simple, DistanceProximity, LimitedProximity, Accelerated }

    void Start()
    {
        if (homingType == HomingType.Simple)
        {
            var bulletPattern = new TransformAction[1]
            {
                new TransformAction
                {
                    Duration = 0.1f,
                    StartTimer = 0,

                    Action = SimpleHoming,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true
                },
            };

            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
        else if (homingType == HomingType.DistanceProximity)
        {
            var bulletPattern = new TransformAction[1]
            {
                new TransformAction
                {
                    Duration = 0.1f,
                    StartTimer = 0,

                    Action = DistanceProximityHoming,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true
                },
            };

            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
        else if (homingType == HomingType.LimitedProximity)
        {
            var bulletPattern = new TransformAction[1]
            {
                new TransformAction
                {
                    Duration = 0.1f,
                    StartTimer = 0,

                    Action = LimitedProximityHoming,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true
                },
            };

            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
        else if (homingType == HomingType.Accelerated)
        {
            var bulletPattern = new TransformAction[1]
            {
                new TransformAction
                {
                    Duration = 0.1f,
                    StartTimer = 0,

                    Action = AcceleratedHoming,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true
                },
            };

            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
    }

    /// <summary>
    /// Simple Homing
    /// 1. Rotate towards the target by a small amount
    /// 2. Move Forward
    /// </summary>
    public TransformData SimpleHoming(TransformData startData, float speed, float time)
    {
        if (homingTarget)
        {
            var dirToPlayer = (homingTarget.position - startData.Position).normalized;

            startData.Rotation = Quaternion.RotateTowards(startData.Rotation, Quaternion.LookRotation(dirToPlayer), HomingRate * (speed * time));
        }

        Vector3 forward = startData.Rotation * Vector3.forward * (speed * time);

        startData.Position = startData.Position + forward;

        return startData;
    }

    /// <summary>
    /// Distance Proximity Homing
    /// 1. Rotate towards the target by a small amount when within proximity radius
    /// 2. Move Forward
    /// </summary>
    public TransformData DistanceProximityHoming(TransformData startData, float speed, float time)
    {
        if (homingTarget)
        {
            bool targetInRange = Vector3.Distance(startData.Position, homingTarget.position) <= ProximityDistance;

            if (targetInRange)
            {
                var dirToPlayer = (homingTarget.position - startData.Position).normalized;

                startData.Rotation = Quaternion.RotateTowards(startData.Rotation, Quaternion.LookRotation(dirToPlayer), HomingRate * (speed * time));
            }
        }

        Vector3 forward = startData.Rotation * Vector3.forward * (speed * time);

        startData.Position = startData.Position + forward;

        return startData;
    }

    /// <summary>
    /// Limited Proximity Homing
    /// 1. Rotate towards the target by a small amount when within certain angle from a direction, 
    /// 2. Move Forward
    /// </summary>
    public TransformData LimitedProximityHoming(TransformData startData, float speed, float time)
    {
        if (homingTarget)
        {
            var dirToPlayer = (homingTarget.position - startData.Position).normalized;

            bool targetInRange = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dirToPlayer)) <= HomingRate * LimitedProximityFactor;

            if (targetInRange)
            {
                startData.Rotation = Quaternion.RotateTowards(startData.Rotation, Quaternion.LookRotation(dirToPlayer), HomingRate * (speed * time));
            }
        }

        Vector3 forward = startData.Rotation * Vector3.forward * (speed * time);

        startData.Position = startData.Position + forward;

        return startData;
    }

    /// <summary>
    /// Accelerated Homing
    /// 1. Rotate towards the target by a small amount.
    /// 2. Move Forward, the closer the target, the faster it moves forward
    /// </summary>
    public TransformData AcceleratedHoming(TransformData startData, float speed, float time)
    {
        if (homingTarget)
        {
            var dirToPlayer = (homingTarget.position - startData.Position).normalized;

            startData.Rotation = Quaternion.RotateTowards(startData.Rotation, Quaternion.LookRotation(dirToPlayer), HomingRate * (speed * time));

            // Linear Acceleration growth based on distance
            var forwardSpeed = speed * Mathf.Clamp(AcceleratedRadius / Vector3.Distance(startData.Position, homingTarget.position), 1, AccelerationMulti);
            Vector3 forward = startData.Rotation * Vector3.forward * (forwardSpeed * time);

            startData.Position = startData.Position + forward;
        }

        return startData;
    }
}
