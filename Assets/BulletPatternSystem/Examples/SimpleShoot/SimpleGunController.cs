using UnityEngine;

public class SimpleGunController : MonoBehaviour
{
    [SerializeField] GunStats baseStats;
    [SerializeField] Gun gun;

    void Awake()
    {
        var bulletPattern = BulletPatterns.Straight(baseStats.Power);
        var stats = Instantiate(baseStats);
        gun.SetupShoot(bulletPattern, stats);
    }
}