using UnityEngine;

public class SimpleShootSystemController : MonoBehaviour
{
    [SerializeField] ShootSystemData BaseShootSystemStats;

    [SerializeField] ShootSystem Gun;

    [Header("Shoot Properties")]
    [SerializeField] float ShootPower = 2;

    void Awake()
    {
        var bulletPattern = BulletPatterns.Straight(ShootPower);
        var stats = Instantiate(BaseShootSystemStats);
        Gun.SetupShoot(bulletPattern, stats);
    }
}