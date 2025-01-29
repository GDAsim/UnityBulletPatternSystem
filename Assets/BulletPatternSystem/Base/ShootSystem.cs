using UnityEngine;

public class ShootSystem : MonoBehaviour
{ 
    [SerializeField] Ammo AmmoPrefab;

    [Header("Optional")]
    [SerializeField] Transform CustomFirePos;
    ShootSystemData systemStats;

    int currentMagazineCount;
    int currentAmmoCount;
    float fireTimer;
    float reloadTimer;
    bool HaveAmmo => currentAmmoCount > 0;
    bool HaveMag => currentMagazineCount > 0;

    TransformAction[] systemPattern;
    int currentIndex = 0;

    TransformAction currentAction;
    float actionTimer = 0;

    TransformAction[] bulletPattern;
    Transform firetransform;

    public void SetupPreShoot(TransformAction[] systemPattern,
    float StartSystemDelay = 0)
    {

    }
    public void SetupShoot(TransformAction[] bulletPattern, ShootSystemData shootStats,
        float StartShootDelay = 0)
    {
        this.bulletPattern = bulletPattern;
        this.systemStats = shootStats;

        if (CustomFirePos != null)
        {
            firetransform = CustomFirePos;
        }
        else
        {
            firetransform = transform;
        }

        currentMagazineCount = systemStats.MagazineCount;
        currentAmmoCount = systemStats.MagazineCapacity;
        fireTimer = systemStats.ReloadDelay - StartShootDelay;
        reloadTimer = 0;
    }


    void Update()
    {
        PreFireAction();

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

    void PreFireAction()
    {
        if (systemPattern == null || systemPattern.Length == 0)
        {
            Debug.LogWarning("No Pattern Set", this);
            return;
        }

        var dt = Time.deltaTime;

        if (actionTimer <= 0)
        {
            currentAction.EndAction();

            currentAction = systemPattern[currentIndex];
            currentAction.GetReady(transform);
            actionTimer = currentAction.Duration;

            currentIndex++;
            if (currentIndex == systemPattern.Length) currentIndex = 0;
        }

        currentAction.DoTransformAction(dt);
        actionTimer -= dt;
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
        firetransform.GetPositionAndRotation(out Vector3 pos, out Quaternion rot);

        var ammo = Instantiate(AmmoPrefab, pos, rot);

        ammo.Setup(bulletPattern);

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