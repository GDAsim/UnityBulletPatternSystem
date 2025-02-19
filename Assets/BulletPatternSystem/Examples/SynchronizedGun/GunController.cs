namespace SynchronizedGun
{
    using System;
    using UnityEngine;

    public class GunController : MonoBehaviour
    {
        [SerializeField] GunStats baseStats;
        [SerializeField] PatternSelect patternSelect;

        [SerializeField] Gun gun;

        [SerializeField] Transform Pos1;
        [SerializeField] Transform Pos2;
        [SerializeField] Transform Pos3;
        [SerializeField] Transform Pos4;

        enum PatternSelect { ShootMoveSync, BulletMoveSync }

        void Awake()
        {
            IAction[] gunPattern;
            IAction[] bulletPattern;
            var stats = Instantiate(baseStats);
            switch (patternSelect)
            {
                case PatternSelect.ShootMoveSync:
                    gunPattern = new IAction[]
                    {
                        new TransformAction
                        {
                            Duration = baseStats.ShootDelay,
                            StartTime = 0,

                            Action = TranslateMove1,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = false
                        },
                        new TransformAction
                        {
                            Duration = baseStats.ShootDelay,
                            StartTime = 0,

                            Action = TranslateMove2,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = false
                        },
                        new TransformAction
                        {
                            Duration = baseStats.ShootDelay,
                            StartTime = 0,

                            Action = TranslateMove3,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = false
                        },
                        new TransformAction
                        {
                            Duration = baseStats.ShootDelay,
                            StartTime = 0,

                            Action = TranslateMove4,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = false
                        },
                    };
                    bulletPattern = BulletPatterns.Straight(baseStats.Power);
                    break;
                case PatternSelect.BulletMoveSync:
                    gunPattern = new IAction[]
                    {
                        new TransformAction
                        {
                            Duration = baseStats.ShootDelay,
                            StartTime = 0,

                            Action = TranslateMove1,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = false
                        },
                        new TransformAction
                        {
                            Duration = baseStats.ShootDelay,
                            StartTime = 0,

                            Action = TranslateMove2,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = false
                        },
                        new TransformAction
                        {
                            Duration = baseStats.ShootDelay,
                            StartTime = 0,

                            Action = TranslateMove3,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = false
                        },
                        new TransformAction
                        {
                            Duration = baseStats.ShootDelay,
                            StartTime = 0,

                            Action = TranslateMove4,
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = false
                        },
                    };
                    bulletPattern = new IAction[]
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
                            ActionSpeed = baseStats.Power,
                            IsDeltaAction = true,
                        },
                    };
                    gun.SetupShoot(bulletPattern, stats);
                    break;
                default:
                    throw new NotImplementedException();
            }
            gun.SetupPreShoot(gunPattern);
            gun.SetupShoot(bulletPattern, stats);
        }

        bool Has4ShootCycleEnd()
        {
            return gun.TotalShootCount % baseStats.MagazineCapacity == 0;
        }

        TransformData TranslateMove1(TransformData startData, float speed, float time)
        {
            float lerpTime;
            if (time == 0) lerpTime = 1;
            else lerpTime = time / baseStats.ShootDelay;

            startData.Position = Vector3.Lerp(startData.Position, Pos1.localPosition, lerpTime);

            return startData;
        }
        TransformData TranslateMove2(TransformData startData, float speed, float time)
        {
            float lerpTime;
            if (time == 0) lerpTime = 1;
            else lerpTime = time / baseStats.ShootDelay;

            startData.Position = Vector3.Lerp(startData.Position, Pos2.localPosition, lerpTime);

            return startData;
        }
        TransformData TranslateMove3(TransformData startData, float speed, float time)
        {
            float lerpTime;
            if (time == 0) lerpTime = 1;
            else lerpTime = time / baseStats.ShootDelay;

            startData.Position = Vector3.Lerp(startData.Position, Pos3.localPosition, lerpTime);

            return startData;
        }
        TransformData TranslateMove4(TransformData startData, float speed, float time)
        {
            float lerpTime;
            if (time == 0) lerpTime = 1;
            else lerpTime = time / baseStats.ShootDelay;

            startData.Position = Vector3.Lerp(startData.Position, Pos4.localPosition, lerpTime);

            return startData;
        }
    }
}