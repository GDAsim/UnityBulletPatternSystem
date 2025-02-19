namespace TeleportGun
{
    using UnityEngine;

    public class GunController : MonoBehaviour
    {
        [SerializeField] GunStats baseStats;
        [SerializeField] PatternSelect patternSelect;

        [SerializeField] Gun gun;

        enum PatternSelect
        {
            InstantAction, // Apply One Time
            JumpAction // Start Action with a StartTime
        }

        void Awake()
        {
            var stats = Instantiate(baseStats);
            IAction[] bulletPattern = null;

            switch (patternSelect)
            {
                case PatternSelect.InstantAction:
                    bulletPattern = new IAction[4]
                    {
                    new TransformAction
                    {
                        Duration = 1,
                        StartTime = 0,

                        Action = TransformAction.MoveForward,
                        ActionSpeed = baseStats.Power,
                        IsDeltaAction = true,
                    },
                    new TransformAction
                    {
                        Action = InstantTeleportRight,
                        ActionSpeed = baseStats.Power,
                        IsDeltaAction = false,
                    },
                    new TransformAction
                    {
                        Duration = 1,
                        StartTime = 0,

                        Action = TransformAction.MoveForward,
                        ActionSpeed = baseStats.Power,
                        IsDeltaAction = true,
                    },
                    new TransformAction
                    {
                        Action = InstantTeleportLeft,
                        ActionSpeed = baseStats.Power,
                        IsDeltaAction = false,
                    },
                    };
                    gun.SetupShoot(bulletPattern, stats);
                    break;
                case PatternSelect.JumpAction:
                    bulletPattern = new IAction[2]
                    {
                    new TransformAction
                    {
                        Duration = 1,
                        StartTime = 0,

                        Action = TransformAction.MoveForward,
                        ActionSpeed = baseStats.Power,
                        IsDeltaAction = true,
                    },
                    new TransformAction
                    {
                        Duration = 1,
                        StartTime = 1,

                        Action = TransformAction.MoveForward,
                        ActionSpeed = baseStats.Power,
                        IsDeltaAction = false,
                    },
                    };
                    gun.SetupShoot(bulletPattern, stats);
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
}