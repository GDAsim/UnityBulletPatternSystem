using UnityEngine;

public partial struct TransformAction
{
    public static TransformData MoveForward1x(TransformData startData, float time)
    {
        var forward = startData.Rotation * Vector3.forward * time;

        startData.Position = startData.Position + forward;

        return startData;
    }

    public static TransformData MoveForward2x(TransformData startData, float time)
    {
        var forward = startData.Rotation * Vector3.forward * time;

        startData.Position = startData.Position + forward;

        return startData;
    }
}
