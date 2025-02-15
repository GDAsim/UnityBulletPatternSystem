using System;
using UnityEngine;

public class SimpleGunController : MonoBehaviour
{
    [SerializeField] GunStats baseStats;
    [SerializeField] Gun gun;
    [SerializeField] GunPatternSelect PatternSelect;

    enum GunPatternSelect
    {
        Straight,
        Sine
    }

    void Awake()
    {
        IAction[] bulletPattern;
        switch (PatternSelect)
        {
            case GunPatternSelect.Straight:
                bulletPattern = BulletPatterns.Straight(baseStats.Power);
                break;
            case GunPatternSelect.Sine:
                bulletPattern = BulletPatterns.Sine(baseStats.Power, Vector3.left, 1f);
                break;
            default:
                throw new NotImplementedException();
        }
        
        var stats = Instantiate(baseStats);
        gun.SetupShoot(bulletPattern, stats);
    }
}