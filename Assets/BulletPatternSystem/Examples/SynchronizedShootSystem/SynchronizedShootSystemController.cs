using UnityEngine;

public class SynchronizedShootSystemController : MonoBehaviour
{
    [SerializeField] ShootSystemData BaseShootSystemStats;
    [SerializeField] SyncType snycType;

    [SerializeField] ShootSystem Gun;

    [Header("Shoot Properties")]
    [SerializeField] float ShootPower = 2;

    [SerializeField] Transform Pos1;
    [SerializeField] Transform Pos2;
    [SerializeField] Transform Pos3;
    [SerializeField] Transform Pos4;

    enum SyncType { ShootMoveSync, BulletMoveSync }

    void Awake()
    {
        if (snycType == SyncType.ShootMoveSync)
        {
            var systemPattern = new TransformAction[4]
            {
                new TransformAction
                {
                    Duration = BaseShootSystemStats.ShootDelay,
                    StartTime = 0,

                    Action = TranslateMove1,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = false
                },
                new TransformAction
                {
                    Duration = BaseShootSystemStats.ShootDelay,
                    StartTime = 0,

                    Action = TranslateMove2,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = false
                },
                new TransformAction
                {
                    Duration = BaseShootSystemStats.ShootDelay,
                    StartTime = 0,

                    Action = TranslateMove3,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = false
                },
                new TransformAction
                {
                    Duration = BaseShootSystemStats.ShootDelay,
                    StartTime = 0,

                    Action = TranslateMove4,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = false
                },
            };
            Gun.SetupPreShoot(systemPattern);

            var bulletPattern = BulletPatterns.Straight(ShootPower);
            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
        else if (snycType == SyncType.BulletMoveSync)
        {
            var systemPattern = new TransformAction[4]
            {
                new TransformAction
                {
                    Duration = BaseShootSystemStats.ShootDelay,
                    StartTime = 0,

                    Action = TranslateMove1,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = false
                },
                new TransformAction
                {
                    Duration = BaseShootSystemStats.ShootDelay,
                    StartTime = 0,

                    Action = TranslateMove2,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = false
                },
                new TransformAction
                {
                    Duration = BaseShootSystemStats.ShootDelay,
                    StartTime = 0,

                    Action = TranslateMove3,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = false
                },
                new TransformAction
                {
                    Duration = BaseShootSystemStats.ShootDelay,
                    StartTime = 0,

                    Action = TranslateMove4,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = false
                },
            };
            Gun.SetupPreShoot(systemPattern);

            var bulletPattern = new IAction[2]
            {
                new DelayAction
                {
                    DelayUntil = Has4ShootCycleEnd
                },
                new TransformAction
                {
                    Duration = 9999,
                    StartTime = 0,

                    Action = TransformAction.MoveForward,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true,
                },
            };
            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
    }

    bool Has4ShootCycleEnd()
    {
        return Gun.TotalShootCount % BaseShootSystemStats.MagazineCapacity == 0;
    }

    TransformData TranslateMove1(TransformData startData, float speed, float time)
    {
        float lerpTime;
        if (time == 0) lerpTime = 1;
        else lerpTime = time / BaseShootSystemStats.ShootDelay;
        
        startData.Position = Vector3.Lerp(startData.Position, Pos1.localPosition, lerpTime);

        return startData;
    }
    TransformData TranslateMove2(TransformData startData, float speed, float time)
    {
        float lerpTime;
        if (time == 0) lerpTime = 1;
        else lerpTime = time / BaseShootSystemStats.ShootDelay;

        startData.Position = Vector3.Lerp(startData.Position, Pos2.localPosition, lerpTime);

        return startData;
    }
    TransformData TranslateMove3(TransformData startData, float speed, float time)
    {
        float lerpTime;
        if (time == 0) lerpTime = 1;
        else lerpTime = time / BaseShootSystemStats.ShootDelay;

        startData.Position = Vector3.Lerp(startData.Position, Pos3.localPosition, lerpTime);

        return startData;
    }
    TransformData TranslateMove4(TransformData startData, float speed, float time)
    {
        float lerpTime;
        if (time == 0) lerpTime = 1;
        else lerpTime = time / BaseShootSystemStats.ShootDelay;

        startData.Position = Vector3.Lerp(startData.Position, Pos4.localPosition, lerpTime);

        return startData;
    }
}