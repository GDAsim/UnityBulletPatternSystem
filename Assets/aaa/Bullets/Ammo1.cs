using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ammo1 : MonoBehaviour
{
    public Queue<TransformAction> patterns;

    TransformAction currentAction;
    float moveTimer = 0;
    public void Setup()
    {
        patterns = new Queue<TransformAction>();

        var move1 = new TransformAction
        {
            Transform = transform,
            Duration = Mathf.PI / 2,
            StartTimer = 0,
            transformAction = SineMove
        };
        patterns.Enqueue(move1);

        //var move2 = new TransformAction
        //{
        //    Transform = transform,
        //    Duration = 1,
        //    StartTimer = 0,
        //    transformAction = ForwardMove
        //};
        //patterns.Enqueue(move2);

        var move3 = new TransformAction
        {
            Transform = transform,
            Duration = Mathf.PI / 2,
            StartTimer = Mathf.PI / 2,
            transformAction = SineMove
        };
        patterns.Enqueue(move3);
    }

    void Update()
    {
        var dt = Time.deltaTime;

        if (moveTimer <= 0)
        {
            if (patterns.Count > 0)
            {
                transform.GetPositionAndRotation(out var pos, out var rot);
                var scale = transform.localScale;

                TransformData data = new()
                {
                    Position = pos,
                    Rotation = rot,
                    Scale = scale
                };

                currentAction = patterns.Dequeue();
                currentAction.GetReady(data);

                moveTimer = currentAction.Duration;
            }
            else return;
        }
        else
        {
            currentAction.DoTransformAction(dt);

            moveTimer -= dt;
        }
    }

    public static TransformData SineMove(TransformData startData, float time)
    {
        var SineUpDown = new Vector3(0, Mathf.Sin(time), 0);
        var forward = startData.Rotation * Vector3.forward * time;

        startData.Position = startData.Position + forward + SineUpDown;

        return startData;
    }

    public static TransformData ForwardMove(TransformData startData, float time)
    {
        var forward = startData.Rotation * Vector3.forward * time;

        startData.Position = startData.Position + forward;

        return startData;
    }
}


public struct TransformAction
{
    public Transform Transform;
    public Func<TransformData, float, TransformData> transformAction;

    /// <summary>
    /// How Long To Run the Action
    /// </summary>
    public float Duration;

    /// <summary>
    /// Start the timer with a value. Used for certain Action
    /// </summary>
    public float StartTimer;

    TransformData startData;
    float timer;

    /// <summary>
    /// Prep Function - Call Once Before Update
    /// </summary>
    public void GetReady(TransformData startData)
    {
        timer = StartTimer;

        startData = transformAction.Invoke(startData, timer);

        this.startData = startData;
    }

    /// <summary>
    /// Update Function
    /// Delta time is required in case for a custom time implementation
    /// </summary>
    public void DoTransformAction(float deltatime)
    {
        timer += deltatime;
        var transformData = transformAction.Invoke(startData, timer);
        transformData.ApplyTo(Transform);
    }
}