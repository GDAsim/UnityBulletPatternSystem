using UnityEngine;

public class TwinShootSystemController : MonoBehaviour
{
    [SerializeField] ShootSystemData BaseShootSystemStats;
    [SerializeField] float ShootPower = 1;
    [SerializeField] ShootMode shootmode = ShootMode.Cycle;

    [SerializeField] ShootSystem RightGun;
    [SerializeField] ShootSystem LeftGun;

    enum ShootMode { Normal, Cycle , Helix }

    void Start()
    {
        if(shootmode == ShootMode.Normal)
        {
            var bulletPattern = BulletPatterns.StraightPattern(ShootPower);

            var stats = Instantiate(BaseShootSystemStats);
            RightGun.Setup(bulletPattern, stats);
            LeftGun.Setup(bulletPattern, stats);
        }
        else if (shootmode == ShootMode.Cycle)
        {
            var bulletPattern = BulletPatterns.StraightPattern(ShootPower);

            var stats = Instantiate(BaseShootSystemStats);

            var leftGunShootDelay = stats.ShootDelay;
            stats.ShootDelay *= 2;

            RightGun.Setup(bulletPattern, stats);
            LeftGun.Setup(bulletPattern, stats, leftGunShootDelay);
        }
        else if (shootmode == ShootMode.Helix)
        {
            var bulletPattern = BulletPatterns.SinePattern(ShootPower, Vector3.up, 0.5f);

            var stats = Instantiate(BaseShootSystemStats);

            var leftGunShootDelay = stats.ShootDelay;
            stats.ShootDelay *= 2;

            RightGun.Setup(bulletPattern, stats);
            LeftGun.Setup(bulletPattern, stats, leftGunShootDelay);
        }
    }
}
