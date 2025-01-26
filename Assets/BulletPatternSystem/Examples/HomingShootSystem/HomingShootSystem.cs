using System;
using UnityEngine;

/// <summary>
/// Shoots one bullet style
/// </summary>
public class HomingShootSystem : MonoBehaviour
{
    [SerializeField] Ammo AmmoPrefab;
    [SerializeField] Transform FirePos;

    [SerializeField] ShootSystemData systemStats;

    [SerializeField] Transform homingTarget;

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

        var pattern = new TransformAction[1]
        {
            new TransformAction
            {
                Duration = 0.1f,
                StartTimer = 0,

                Action = CustomHoming,
                ActionSpeed = 20,
                IsDeltaAction = true
            }
        };

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

    public TransformData ForwardHoming(TransformData startData, float speed, float time)
    {
        if (homingTarget)
        {
            var dir = (homingTarget.position - startData.Position).normalized;
            var dirMove = dir * (speed * time);

            startData.Position = startData.Position + dirMove; 
        }

        return startData;
    }
    public TransformData CustomHoming(TransformData startData, float speed, float time)
    {
        if (homingTarget)
        {
            var dirToPlayer = (homingTarget.position - startData.Position).normalized;
            startData.Rotation = Quaternion.RotateTowards(startData.Rotation, Quaternion.LookRotation(dirToPlayer), speed * 10 * time);

            Vector3 forward = startData.Rotation * Vector3.forward * (speed * time);

            startData.Position = startData.Position + forward;
        }

        return startData;
    }
}
