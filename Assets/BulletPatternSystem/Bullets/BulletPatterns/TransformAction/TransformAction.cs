using System;
using UnityEngine;

public partial struct TransformAction
{
    public Func<TransformData, float, float, TransformData> Action;
    
    /// <summary>
    /// Used when the Performing Certain Action has a StartTimer != 0
    /// </summary>
    public bool IsDeltaAction;

    /// <summary>
    /// How Long To Run the Action
    /// </summary>
    public float Duration;

    /// <summary>
    /// Base Speed Agument To Tell Action how fast to run
    /// </summary>
    public float ActionSpeed;

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

        transform.GetLocalPositionAndRotation(out var pos, out var rot);
        var scale = transform.localScale;

        startData = new(transform);

        timer = StartTimer;

        prevData = Action.Invoke(startData, ActionSpeed, StartTimer);
    }

    /// <summary>
    /// Update Function
    /// Delta time is required in case for a custom time implementation
    /// </summary>
    public void DoTransformAction(float deltatime)
    {
        timer += deltatime;

        var transformData = Action.Invoke(startData, ActionSpeed, timer);

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

        var transformData = Action.Invoke(startData, ActionSpeed, StartTimer + Duration);

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