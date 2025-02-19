namespace TwinGun
{
    using UnityEngine;
    public class GunController : MonoBehaviour
    {
        [SerializeField] GunStats baseStats;
        [SerializeField] PatternSelect shootmode;

        [SerializeField] Gun RightGun;
        [SerializeField] Gun LeftGun;

        enum PatternSelect { Normal, Cycle, Helix }

        void Awake()
        {
            if (shootmode == PatternSelect.Normal)
            {
                var bulletPattern = BulletPatterns.Straight(baseStats.Power);

                var stats = Instantiate(baseStats);
                RightGun.SetupShoot(bulletPattern, stats);
                LeftGun.SetupShoot(bulletPattern, stats);
            }
            else if (shootmode == PatternSelect.Cycle)
            {
                var bulletPattern = BulletPatterns.Straight(baseStats.Power);

                var stats = Instantiate(baseStats);
                stats.ShootDelay *= 2;

                RightGun.SetupShoot(bulletPattern, stats);

                stats.StartShootDelay = 1;

                LeftGun.SetupShoot(bulletPattern, stats);
            }
            else if (shootmode == PatternSelect.Helix)
            {
                var RightbulletPattern = BulletPatterns.Sine(baseStats.Power, Vector3.right, 0.2f);
                var LeftbulletPattern = BulletPatterns.Sine(baseStats.Power, Vector3.left, 0.2f);

                var stats = Instantiate(baseStats);

                RightGun.SetupShoot(RightbulletPattern, stats);
                LeftGun.SetupShoot(LeftbulletPattern, stats);
            }
        }
    }

}

