using System;
using UnityEngine;

public partial struct TransformAction
{
    public Func<TransformData, float, TransformData> Action;
    public bool IsDeltaAction;

    /// <summary>
    /// How Long To Run the Action
    /// </summary>
    public float Duration;

    /// <summary>
    /// Start the timer with a value. Used for certain Action
    /// </summary>
    public float StartTimer;

    Transform transform;
    TransformData startData;
    TransformData prevData;
    float timer;

    /// <summary>
    /// Prep Function - Call Once Before Update
    /// </summary>
    public void GetReady(Transform transform)
    {
        this.transform = transform;

        transform.GetPositionAndRotation(out var pos, out var rot);
        var scale = transform.localScale;

        TransformData startData = new()
        {
            Position = pos,
            Rotation = rot,
            Scale = scale
        };

        timer = StartTimer;

        prevData = Action.Invoke(startData, StartTimer);

        this.startData = startData;
    }

    /// <summary>
    /// Update Function
    /// Delta time is required in case for a custom time implementation
    /// </summary>
    public void DoTransformAction(float deltatime)
    {
        timer += deltatime;

        var transformData = Action.Invoke(startData, timer);

        if (IsDeltaAction)
        {
            var delta = transformData - prevData;
            delta.AddTo(transform);
        }
        else
        {
            transformData.ApplyTo(transform);
        }

        prevData = transformData;
    }

    /// <summary>
    /// Clean Up
    /// </summary>
    public void EndAction()
    {
        if (Action == null) return;

        var transformData = Action.Invoke(startData, StartTimer + Duration);

        if (IsDeltaAction)
        {
            var delta = transformData - prevData;
            delta.AddTo(transform);
        }
        else
        {
            transformData.ApplyTo(transform);
        }
    }
}