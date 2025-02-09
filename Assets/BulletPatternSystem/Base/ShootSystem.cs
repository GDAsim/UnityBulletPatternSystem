using UnityEngine;

public class ShootSystem : MonoBehaviour
{ 
    [SerializeField] Ammo AmmoPrefab;

    [Header("Optional")]
    [SerializeField] Transform CustomFirePos;
    ShootSystemData systemStats;

    int currentMagazineCount;
    int currentAmmoCount;
    float shootTimer;
    float reloadTimer;
    bool HaveAmmo => currentAmmoCount > 0;
    bool HaveMag => currentMagazineCount > 0;
    public int TotalShootCount { get; set; }

    TransformAction[] systemPattern;
    int currentIndex = 0;

    TransformAction currentAction;
    float actionTimer = 0;

    IAction[] bulletPattern;
    Transform firetransform;

    public void SetupPreShoot(TransformAction[] systemPattern,
        float StartSystemDelay = 0)
    {
        this.systemPattern = systemPattern;

        currentAction = systemPattern[currentIndex++];
        currentAction.ReadyAction(transform);
        actionTimer = -StartSystemDelay;
        if (currentIndex == systemPattern.Length) currentIndex = 0;
    }
    public void SetupShoot(IAction[] bulletPattern, ShootSystemData shootStats,
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
        shootTimer = -StartShootDelay;
        reloadTimer = 0;
    }
    void Update()
    {
        PreShootAction();

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

    void PreShootAction()
    {
        if (systemPattern == null || systemPattern.Length == 0)
        {
            //Debug.LogWarning("No System Pattern Set", this);
            return;
        }

        var dt = Time.deltaTime;

        if (actionTimer >= currentAction.Duration && currentAmmoCount > 0)
        {
            currentAction.EndAction();

            currentAction = systemPattern[currentIndex++];
            currentAction.ReadyAction(transform);
            actionTimer = 0;
            if (currentIndex == systemPattern.Length) currentIndex = 0;
        }

        currentAction.DoAction(dt);
        actionTimer += dt;
    }

    void AttemptShoot()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= systemStats.ShootDelay)
        {
            shootTimer = 0;

            Fire();
        }
    }
    void Fire()
    {
        firetransform.GetPositionAndRotation(out Vector3 pos, out Quaternion rot);

        var ammo = Instantiate(AmmoPrefab, pos, rot);

        ammo.Setup(bulletPattern);

        currentAmmoCount--;

        TotalShootCount++;
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