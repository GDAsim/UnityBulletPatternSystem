using UnityEngine;

public class SplitterShootSystemController : MonoBehaviour
{
    [SerializeField] ShootSystemData BaseShootSystemStats;

    [SerializeField] ShootSystem Gun;

    [Header("Shoot Properties")]
    [SerializeField] float ShootPower = 2;

    void Awake()
    {
        var bulletPattern = new IAction[2]
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
                DestroyOnEnd = true,
            },
        };
        
        var stats = Instantiate(BaseShootSystemStats);
        Gun.SetupShoot(bulletPattern, stats);
    }
}