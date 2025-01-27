using UnityEngine;

/// <summary>
/// Basic Transform Action - call back function for bullet patterns to update it's transform(data)
/// </summary>
public partial struct TransformAction
{
    public static TransformData MoveForward(TransformData startData, float speed, float time)
    {
        var forward = startData.Rotation * Vector3.forward * (speed * time);

        startData.Position = startData.Position + forward;

        return startData;
    }
}
