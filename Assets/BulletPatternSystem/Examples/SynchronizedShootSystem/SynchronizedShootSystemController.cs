using UnityEngine;

public class SynchronizedShootSystemController : MonoBehaviour
{
    [SerializeField] ShootSystemData BaseShootSystemStats;
    [SerializeField] HomingType homingType;

    [SerializeField] ShootSystem Gun;

    [Header("Shoot Properties")]
    [SerializeField] float ShootPower = 2;

    enum HomingType { Simple }

    void Start()
    {
        if (homingType == HomingType.Simple)
        {
            var bulletPattern = BulletPatterns.Straight(ShootPower);

            var stats = Instantiate(BaseShootSystemStats);
            Gun.SetupShoot(bulletPattern, stats);
        }
    }
}