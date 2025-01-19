using System;

public static class BulletPatterns
{
    public static TransformAction[] TestPattern => new TransformAction[4]
    {
        new TransformAction
        {
            Duration = MathF.PI,
            StartTimer = 0,

            Action = TransformAction.SineMove1x,
            IsDeltaAction = true
        },
        new TransformAction
        {
            Duration = MathF.PI,
            StartTimer = MathF.PI,

            Action = TransformAction.SineMove1x,
            IsDeltaAction = true
        },
        new TransformAction
        {
            Duration = 2,
            StartTimer = 2,

            Action = TransformAction.MoveForward1x
        },
        new TransformAction
        {
            Duration = 2,
            StartTimer = 2,

            Action = TransformAction.MoveForward1x
        },
    };
}
