namespace SplitterGun
{
    using System;
    using UnityEngine;

    public class GunController : MonoBehaviour
    {
        [SerializeField] GunStats baseStats;
        [SerializeField] Gun gun;

        [SerializeField] PatternSelect patternSelect;

        [Header("Extra")]
        [SerializeField] Ammo ExtraAmmoPrefab;

        [Header("Split")]
        [SerializeField] Ammo SplitAmmoPrefab;

        [Header("Clone")]
        [SerializeField] Ammo CloneAmmoPrefab;

        enum PatternSelect
        {
            Extra, // Spawn a new predefined object
            Split, // Spawn a new predefined object and Destroy Itself
            Clone, // Spawn a new object as itself
            Copy // Spawn a new object as itself and copy current state over
        }

        void Awake()
        {
            IAction[] bulletPattern;
            var stats = Instantiate(baseStats);
            switch (patternSelect)
            {
                case PatternSelect.Extra:
                    bulletPattern = new IAction[]
                    {
                        new TransformAction
                        {
                            Duration = 1,
                            StartTime = 0,

                            Action = TransformAction.MoveForward,
                            ActionSpeed = baseStats.Power,
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
                                    ActionSpeed = baseStats.Power,
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
                                    ActionSpeed = baseStats.Power,
                                    IsDeltaAction = true,
                                },
                            },
                            splitDelta = new TransformData()
                            {
                                Rotation = Quaternion.AngleAxis(-45, transform.up),
                            }
                        },
                    };
                    break;
                case PatternSelect.Split:
                    bulletPattern = new IAction[]
                    {
                        new TransformAction
                        {
                            Duration = 1,
                            StartTime = 0,

                            Action = TransformAction.MoveForward,
                            ActionSpeed = baseStats.Power,
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
                                    ActionSpeed = baseStats.Power,
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
                                    ActionSpeed = baseStats.Power,
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
                    break;
                case PatternSelect.Clone:
                    bulletPattern = new IAction[]
                    {
                        new TransformAction
                        {
                            Duration = 1,
                            StartTime = 0,

                            Action = TransformAction.MoveForward,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = true,
                        },
                        new SplitAction
                        {
                            splitDelta = new TransformData()
                            {
                                Rotation = Quaternion.AngleAxis(45, transform.up),
                            },
                        },
                        new SplitAction
                        {
                            splitDelta = new TransformData()
                            {
                                Rotation = Quaternion.AngleAxis(-45, transform.up),
                            },
                        },
                        new TransformAction
                        {
                            Duration = Mathf.Infinity,
                            StartTime = 0,

                            Action = TransformAction.MoveForward,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = true,
                        },
                    };
                    break;
                case PatternSelect.Copy:
                    bulletPattern = new IAction[]
                    {
                        new TransformAction
                        {
                            Duration = 1,
                            StartTime = 0,

                            Action = TransformAction.MoveForward,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = true,
                        },
                        new SplitAction
                        {
                            splitDelta = new TransformData()
                            {
                                Rotation = Quaternion.AngleAxis(45 + 45, transform.up),
                            },
                            IsCopy = true,
                        },
                        new SplitAction
                        {
                            splitDelta = new TransformData()
                            {
                                Rotation = Quaternion.AngleAxis(-45, transform.up),
                            },
                            IsCopy = true,
                            DestroyOnEnd = true
                        },
                    };
                    break;
                default:
                    throw new NotImplementedException();
            }
            gun.SetupShoot(bulletPattern, stats);
        }
    }
}
