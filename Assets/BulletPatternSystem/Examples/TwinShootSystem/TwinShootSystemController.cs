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
            var bulletPattern = BulletPatterns.Straight(ShootPower);

            var stats = Instantiate(BaseShootSystemStats);
            RightGun.Setup(bulletPattern, stats);
            LeftGun.Setup(bulletPattern, stats);
        }
        else if (shootmode == ShootMode.Cycle)
        {
            var bulletPattern = BulletPatterns.Straight(ShootPower);

            var stats = Instantiate(BaseShootSystemStats);

            var leftGunShootDelay = stats.ShootDelay;
            stats.ShootDelay *= 2;

            RightGun.Setup(bulletPattern, stats);
            LeftGun.Setup(bulletPattern, stats, leftGunShootDelay);
        }
        else if (shootmode == ShootMode.Helix)
        {
            var LeftbulletPattern = BulletPatterns.Sine(ShootPower, Vector3.left, 0.2f);
            var RightbulletPattern = BulletPatterns.Sine(ShootPower, Vector3.right, 0.2f);

            var stats = Instantiate(BaseShootSystemStats);

            RightGun.Setup(RightbulletPattern, stats);
            LeftGun.Setup(LeftbulletPattern, stats);
        }
    }
}
