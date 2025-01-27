using UnityEngine;

/// <summary>
/// Shoots one bullet style
/// </summary>
public class BasicShootSystem : MonoBehaviour
{
    [SerializeField] Ammo AmmoPrefab;
    [SerializeField] Transform FirePos;

    [SerializeField] ShootSystemData systemStats;

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

        ammo.Setup(BulletPatterns.Straight(2));

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

    // Debug
    //void OnDrawGizmos()
    //{

    //}
}
