using UnityEngine;

public class Gun : MonoBehaviour
{ 
    [SerializeField] Ammo AmmoPrefab;

    [Header("Optional")]
    [SerializeField] Transform CustomFirePos;
    GunStats systemStats;

    int currentMagazineCount;
    int currentAmmoCount;
    float shootTimer;
    float reloadTimer;
    bool HaveAmmo => currentAmmoCount > 0;
    bool HaveMag => currentMagazineCount > 0;
    public int TotalShootCount { get; set; }

    public IAction[] Patterns { get; private set; }
    int currentIndex;

    ActionTypes currentActionType;
    TransformAction currentTransformAction;
    DelayAction currentDelayAction;
    //SplitAction currentSplitAction;

    float currentActionTimer;

    IAction[] bulletPattern;
    Transform firetransform;

    public void SetupPreShoot(IAction[] patterns,
        float StartSystemDelay = 0)
    {
        Patterns = patterns;
        currentActionTimer = -StartSystemDelay;

        GetNextAction();

        ReadyAction();
    }
    public void SetupShoot(IAction[] bulletPattern, GunStats shootStats)
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
        shootTimer = -shootStats.StartShootDelay;
        reloadTimer = 0;
    }
    void Update()
    {
        PreShootAction();

        shootTimer += Time.deltaTime;

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
        if (Patterns == null || Patterns.Length == 0) return;

        currentActionTimer += Time.deltaTime;

        if (DoAction())
        {
            EndAction();

            GetNextAction();

            ReadyAction();

            currentActionTimer = 0;
        }
    }

    void GetNextAction()
    {
        switch (Patterns[currentIndex])
        {
            case TransformAction action:
                currentTransformAction = action;
                currentActionType = ActionTypes.TransformAction;
                break;
            case DelayAction action:
                currentDelayAction = action;
                currentActionType = ActionTypes.DelayAction;
                break;
            //case SplitAction action:
            //    currentSplitAction = action;
            //    currentActionType = ActionTypes.SplitAction;
            //    break;
        }

        if (++currentIndex == Patterns.Length) currentIndex = 0;
    }
    void ReadyAction()
    {
        switch (currentActionType)
        {
            case ActionTypes.TransformAction:
                currentTransformAction.ReadyAction(transform);
                return;
            case ActionTypes.DelayAction:
                currentDelayAction.ReadyAction();
                return;
            //case ActionTypes.SplitAction:
            //    currentSplitAction.ReadyAction(this);
            //    return;
        }
    }
    bool DoAction()
    {
        switch (currentActionType)
        {
            case ActionTypes.TransformAction:
                currentTransformAction.DoAction(Time.deltaTime);
                return currentActionTimer >= currentTransformAction.Duration;
            case ActionTypes.DelayAction:
                currentDelayAction.DoAction();
                return currentActionTimer >= currentDelayAction.Duration;
            //case ActionTypes.SplitAction:
            //    currentSplitAction.DoAction();
            //    return true;
            default:
                throw new System.Exception("Not Implemented");
        }
    }
    void EndAction()
    {
        switch (currentActionType)
        {
            case ActionTypes.TransformAction:
                currentTransformAction.EndAction();
                return;
            case ActionTypes.DelayAction:
                currentDelayAction.EndAction();
                return;
            //case ActionTypes.SplitAction:
            //    currentSplitAction.EndAction();
            //    return;
        }
    }

    void AttemptShoot()
    {
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