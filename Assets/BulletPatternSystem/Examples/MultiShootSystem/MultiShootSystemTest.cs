using UnityEngine;

public class MultiShootSystemTest : MonoBehaviour
{ 
    [SerializeField] Ammo AmmoPrefab;
    [SerializeField] Transform FirePos;
    [SerializeField] ShootSystemData systemStats;

    [Header("Shoot Properties")]
    [SerializeField] float ShootPower = 2;

    [Header("MultiShoot Properties")]
    [SerializeField] MultiShootType multiShootType;

    enum MultiShootType { Single, Twin, Triple, Helix }

    int currentMagazineCount;
    int currentAmmoCount;
    float fireTimer;
    float reloadTimer;

    bool HaveAmmo => currentAmmoCount > 0;
    bool HaveMag => currentMagazineCount > 0;

    void Start() => OnValidate();
    void OnValidate()
    {
        currentMagazineCount = systemStats.MagazineCount;
        currentAmmoCount = systemStats.MagazineCapacity;
        fireTimer = Mathf.Infinity;
        reloadTimer = 0;
    }

    void Update()
    {
        if (HaveAmmo)
        {
            AttemptShoot();
        }
        else
        {
            if (HaveMag)
            {
                AttemptReload();
            }
        }
    }

    void AttemptShoot()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= systemStats.ShootDelay)
        {
            fireTimer = 0;

            Fire();
        }
    }
    void Fire()
    {
        FirePos.GetPositionAndRotation(out Vector3 pos, out Quaternion rot);
        var ammo = Instantiate(AmmoPrefab, pos, rot);

        // TOUPDATE: Define Pattern Here
        TransformAction[] pattern = null;
        if (multiShootType == MultiShootType.Single)
        {
            pattern = new TransformAction[1]
            {
                new TransformAction
                {
                    Duration = 0.1f,
                    StartTimer = 0,

                    Action = BulletPatterns.StraightPattern(2),
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true
                },
            };
        }
        else if (multiShootType == MultiShootType.Twin)
        {
            pattern = new TransformAction[1]
            {
                new TransformAction
                {
                    Duration = 0.1f,
                    StartTimer = 0,

                    Action = DistanceProximityHoming,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true
                },
            };
        }
        else if (multiShootType == MultiShootType.Helix)
        {
            pattern = new TransformAction[1]
            {
                new TransformAction
                {
                    Duration = 0.1f,
                    StartTimer = 0,

                    Action = LimitedProximityHoming,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true
                },
            };
        }
        else if (multiShootType == MultiShootType.Helix)
        {
            pattern = new TransformAction[1]
            {
                new TransformAction
                {
                    Duration = 0.1f,
                    StartTimer = 0,

                    Action = AcceleratedHoming,
                    ActionSpeed = ShootPower,
                    IsDeltaAction = true
                },
            };
        }

        ammo.Setup(pattern);

        currentAmmoCount--;
    }

    void AttemptReload()
    {
        reloadTimer += Time.deltaTime;

        if (reloadTimer >= systemStats.ReloadDelay)
        {
            reloadTimer -= systemStats.ReloadDelay;

            Reload();
        }
    }
    void Reload()
    {
        currentMagazineCount--;
        currentAmmoCount = systemStats.MagazineCapacity;
    }
}
