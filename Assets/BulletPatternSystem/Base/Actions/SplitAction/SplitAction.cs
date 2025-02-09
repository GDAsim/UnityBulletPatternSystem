
using UnityEngine;

public struct SplitAction : IAction
{
    public bool IsClone; // Copy Current State Over
    public bool DestroyOnEnd;
    public GameObject splitPrefab;
    public TransformData splitDelta;

    Ammo main;

    #region Interface
    float IAction.Duration { get => 0; }
    float IAction.ActionSpeed { get => 1; }
    float IAction.StartTimer { get => 0; }
    #endregion

    public void ReadyAction(Ammo main)
    {
        this.main = main;
    }

    public void DoAction()
    {
        var newAmmoLeft = GameObject.Instantiate(main);
        var newAmmoRight = GameObject.Instantiate(main);


        var mainTransform = new TransformData(main.transform);

        var ammoleftTransform = mainTransform;
        ammoleftTransform.Rotation *= Quaternion.AngleAxis(-45, main.transform.up);
        ammoleftTransform.ApplyTo(newAmmoLeft.transform);
        newAmmoLeft.Setup(main.patterns);

        var ammorightTransform = mainTransform;
        ammorightTransform.Rotation *= Quaternion.AngleAxis(45, main.transform.up);
        ammorightTransform.ApplyTo(newAmmoRight.transform);
        newAmmoRight.Setup(main.patterns);
    }
    public void EndAction()
    {
        if(DestroyOnEnd)
        {
            GameObject.Destroy(main.gameObject);
        }
    }
}
