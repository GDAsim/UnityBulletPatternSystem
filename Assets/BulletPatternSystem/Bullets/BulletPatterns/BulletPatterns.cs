using System;

public static partial class BulletPatterns
{
    public static TransformAction[] StraightPattern(float actionSpeed) => new TransformAction[4]
    {
        new TransformAction
        {
            Duration = MathF.PI,
            StartTimer = 0,

            Action = TransformAction.SineMove,
            ActionSpeed = actionSpeed,
            IsDeltaAction = true
        },
        new TransformAction
        {
            Duration = MathF.PI,
            StartTimer = MathF.PI,

            Action = TransformAction.SineMove,
            ActionSpeed = actionSpeed,
            IsDeltaAction = true
        },
        new TransformAction
        {
            Duration = 2,
            StartTimer = 2,

            Action = TransformAction.MoveForward,
            ActionSpeed = actionSpeed,
            IsDeltaAction = false,
        },
        new TransformAction
        {
            Duration = 2,
            StartTimer = 2,

            Action = TransformAction.MoveForward,
            ActionSpeed = actionSpeed,
            IsDeltaAction = false,
        },
    };
}
