using System;
using UnityEngine;

/// <summary>
/// Basic bullet Patterns
/// </summary>
public static class BulletPatterns
{
    public static TransformAction[] Straight(float actionSpeed) => new TransformAction[1]
    {
        new TransformAction
        {
            Duration = 1,
            StartTimer = 0,

            Action = TransformAction.MoveForward,
            ActionSpeed = actionSpeed,
            IsDeltaAction = true,
        }
    };
    public static TransformAction[] Sine(float actionSpeed, Vector3 axis, float height)
    {
        var actions = new TransformAction[1]
        {
            new TransformAction
            {
                Duration = MathF.PI * 2,
                StartTimer = 0,

                Action = SineMove,
                ActionSpeed = actionSpeed,
                IsDeltaAction = true
            }
        };
            
        return actions;

        TransformData SineMove(TransformData startData, float speed, float time)
        {
            var SineUpDown = axis * Mathf.Sin(time) * height * speed;
            var forward = startData.Rotation * Vector3.forward * (speed * time);

            startData.Position = startData.Position + forward + SineUpDown;

            return startData;
        }
    }
}
