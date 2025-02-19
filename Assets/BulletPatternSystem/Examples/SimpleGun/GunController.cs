namespace SimpleGun
{
    using System;
    using UnityEngine;

    public class GunController : MonoBehaviour
    {
        [SerializeField] GunStats baseStats;
        [SerializeField] PatternSelect patternSelect;

        [SerializeField] Gun gun;

        enum PatternSelect { Straight, Sine }

        void Awake()
        {
            IAction[] bulletPattern;
            var stats = Instantiate(baseStats);
            switch (patternSelect)
            {
                case PatternSelect.Straight:
                    bulletPattern = BulletPatterns.Straight(baseStats.Power);
                    break;
                case PatternSelect.Sine:
                    bulletPattern = BulletPatterns.Sine(baseStats.Power, Vector3.left, 1f);
                    break;
                default:
                    throw new NotImplementedException();
            }
            gun.SetupShoot(bulletPattern, stats);
        }
    }
}