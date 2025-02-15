using UnityEngine;

public class TwinGunController : MonoBehaviour
{
    [SerializeField] GunStats BaseShootSystemStats;
    [SerializeField] float ShootPower = 1;
    [SerializeField] ShootMode shootmode = ShootMode.Cycle;

    [SerializeField] Gun RightGun;
    [SerializeField] Gun LeftGun;

    enum ShootMode { Normal, Cycle, Helix }

    void Awake()
    {
        if (shootmode == ShootMode.Normal)
        {
            var bulletPattern = BulletPatterns.Straight(ShootPower);

            var stats = Instantiate(BaseShootSystemStats);
            RightGun.SetupShoot(bulletPattern, stats);
            LeftGun.SetupShoot(bulletPattern, stats);
        }
        else if (shootmode == ShootMode.Cycle)
        {
            var bulletPattern = BulletPatterns.Straight(ShootPower);

            var stats = Instantiate(BaseShootSystemStats);
            stats.ShootDelay *= 2;

            RightGun.SetupShoot(bulletPattern, stats);

            stats.StartShootDelay = 1;

            LeftGun.SetupShoot(bulletPattern, stats);
        }
        else if (shootmode == ShootMode.Helix)
        {
            var RightbulletPattern = BulletPatterns.Sine(ShootPower, Vector3.right, 0.2f);
            var LeftbulletPattern = BulletPatterns.Sine(ShootPower, Vector3.left, 0.2f);

            var stats = Instantiate(BaseShootSystemStats);

            RightGun.SetupShoot(RightbulletPattern, stats);
            LeftGun.SetupShoot(LeftbulletPattern, stats);
        }
    }
}
