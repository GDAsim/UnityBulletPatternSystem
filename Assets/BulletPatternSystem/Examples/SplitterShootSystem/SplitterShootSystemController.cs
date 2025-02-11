using UnityEngine;

public class SplitterShootSystemController : MonoBehaviour
{
    [SerializeField] ShootSystemData BaseShootSystemStats;
    [SerializeField] SplitType splitType;

    [SerializeField] ShootSystem Gun;

    [Header("Shoot Properties")]
    [SerializeField] float ShootPower = 2;

    [Header("Extra")]
    [SerializeField] Ammo ExtraAmmoPrefab;

    [Header("Split")]
    [SerializeField] Ammo SplitAmmoPrefab;

    [Header("Clone")]
    [SerializeField] Ammo CloneAmmoPrefab;

    enum SplitType { Extra, Split, Clone, Copy }

    void Awake()
    {
        IAction[] bulletPattern;
        if (splitType == SplitType.Extra)
        {
            bulletPattern = new IAction[]
            {
                new TransformAction
                {
                    Duration = 1,
                    StartTime = 0,

                    Action = TransformAction.MoveForward,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true,
                },
                new SplitAction
                {
                    splitPrefab = SplitAmmoPrefab,
                    splitActions = new IAction[1]
                    {
                        new TransformAction
                        {
                            Duration = 1,
                            StartTime = 0,

                            Action = TransformAction.MoveForward,
                            ActionSpeed = ShootPower,
                            IsDeltaAction = true,
                        },
                    },
                    splitDelta = new TransformData()
                    {
                        Rotation = Quaternion.AngleAxis(45, transform.up),
                    }
                },
                new SplitAction
                {
                    splitPrefab = SplitAmmoPrefab,
                    splitActions = new IAction[1]
                    {
                        new TransformAction
                        {
                            Duration = 1,
                            StartTime = 0,

                            Action = TransformAction.MoveForward,
                            ActionSpeed = ShootPower,
                            IsDeltaAction = true,
                        },
                    },
                    splitDelta = new TransformData()
                    {
                        Rotation = Quaternion.AngleAxis(-45, transform.up),
                    }
                },
            };

            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
        else if (splitType == SplitType.Split)
        {
            bulletPattern = new IAction[]
            {
                new TransformAction
                {
                    Duration = 1,
                    StartTime = 0,

                    Action = TransformAction.MoveForward,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true,
                },
                new SplitAction
                {
                    splitPrefab = SplitAmmoPrefab,
                    splitActions = new IAction[]
                    {
                        new TransformAction
                        {
                            Duration = 1,
                            StartTime = 0,

                            Action = TransformAction.MoveForward,
                            ActionSpeed = ShootPower,
                            IsDeltaAction = true,
                        },
                    },
                    splitDelta = new TransformData()
                    {
                        Rotation = Quaternion.AngleAxis(45, transform.up),
                    }
                },
                new SplitAction
                {
                    splitPrefab = SplitAmmoPrefab,
                    splitActions = new IAction[]
                    {
                        new TransformAction
                        {
                            Duration = 1,
                            StartTime = 0,

                            Action = TransformAction.MoveForward,
                            ActionSpeed = ShootPower,
                            IsDeltaAction = true,
                        },
                    },
                    splitDelta = new TransformData()
                    {
                        Rotation = Quaternion.AngleAxis(-45, transform.up),
                    },
                    DestroyOnEnd = true
                },
            };

            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
        else if (splitType == SplitType.Clone)
        {
            bulletPattern = new IAction[]
            {
                new TransformAction
                {
                    Duration = 1,
                    StartTime = 0,

                    Action = TransformAction.MoveForward,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true,
                },
                new SplitAction
                {
                    splitDelta = new TransformData()
                    {
                        Rotation = Quaternion.AngleAxis(45, transform.up),
                    },
                    IsClone = true,
                },
                new SplitAction
                {
                    splitDelta = new TransformData()
                    {
                        Rotation = Quaternion.AngleAxis(-45, transform.up),
                    },
                    IsClone = true,
                    DestroyOnEnd = true
                },
                
                new TransformAction
                {
                    Duration = 5,
                    StartTime = 0,

                    Action = TransformAction.MoveForward,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true,
                },
            };

            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
        else if (splitType == SplitType.Copy)
        {
            bulletPattern = new IAction[]
            {
                new TransformAction
                {
                    Duration = 1,
                    StartTime = 0,

                    Action = TransformAction.MoveForward,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true,
                },
                new SplitAction
                {
                    splitDelta = new TransformData()
                    {
                        Rotation = Quaternion.AngleAxis(45, transform.up),
                    },
                },
                new TransformAction
                {
                    Duration = Mathf.Infinity,
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
}