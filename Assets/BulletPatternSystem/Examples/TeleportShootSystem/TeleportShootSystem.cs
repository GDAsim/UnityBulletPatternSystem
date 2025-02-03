using UnityEngine;
using static UnityEngine.Tilemaps.Tilemap;

public class TeleportShootSystem : MonoBehaviour
{
    [SerializeField] ShootSystemData BaseShootSystemStats;
    [SerializeField] TeleportType teleType;

    [SerializeField] ShootSystem Gun;

    [Header("Shoot Properties")]
    [SerializeField] float ShootPower = 2;

    enum TeleportType 
    { 
        InstantAction, // Apply One Time
        JumpAction // Start Action with a StartTime
    }

    void Awake()
    {
        var stats = Instantiate(BaseShootSystemStats);
        IAction[] bulletPattern = null;

        switch (teleType)
        {
            case TeleportType.InstantAction:
                bulletPattern = new IAction[4]
                {
                    new TransformAction
                    {
                        Duration = 1,
                        StartTime = 0,

                        Action = TransformAction.MoveForward,
                        ActionSpeed = ShootPower,
                        IsDeltaAction = true,
                    },
                    new TransformAction
                    {
                        Action = InstantTeleportRight,
                        ActionSpeed = ShootPower,
                        IsDeltaAction = false,
                    },
                    new TransformAction
                    {
                        Duration = 1,
                        StartTime = 0,

                        Action = TransformAction.MoveForward,
                        ActionSpeed = ShootPower,
                        IsDeltaAction = true,
                    },
                    new TransformAction
                    {
                        Action = InstantTeleportLeft,
                        ActionSpeed = ShootPower,
                        IsDeltaAction = false,
                    },
                };
                Gun.SetupShoot(bulletPattern, stats);
                break;
            case TeleportType.JumpAction:
                bulletPattern = new IAction[2]
                {
                    new TransformAction
                    {
                        Duration = 1,
                        StartTime = 0,

                        Action = TransformAction.MoveForward,
                        ActionSpeed = ShootPower,
                        IsDeltaAction = true,
                    },
                    new TransformAction
                    {
                        Duration = 1,
                        StartTime = 1,

                        Action = TransformAction.MoveForward,
                        ActionSpeed = ShootPower,
                        IsDeltaAction = false,
                    },
                };
                Gun.SetupShoot(bulletPattern, stats);
                break;
        }
        

    }

    public TransformData InstantTeleportRight(TransformData startData, float speed, float time)
    {
        var right = startData.Rotation * Vector3.right;

        startData.Position = startData.Position + right;

        return startData;
    }
    public TransformData InstantTeleportLeft(TransformData startData, float speed, float time)
    {
        var left = startData.Rotation * Vector3.left;

        startData.Position = startData.Position + left;

        return startData;
    }
}