
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
        var newAmmo = GameObject.Instantiate(main);

        var mainTransform = new TransformData(main.transform);

        mainTransform.Position += new Vector3(0, 1, 0);
        mainTransform.ApplyTo(newAmmo.transform);

        newAmmo.Setup(main.patterns);
    }
    public void EndAction()
    {
        if(DestroyOnEnd)
        {
            GameObject.Destroy(main.gameObject);
        }
    }
}
