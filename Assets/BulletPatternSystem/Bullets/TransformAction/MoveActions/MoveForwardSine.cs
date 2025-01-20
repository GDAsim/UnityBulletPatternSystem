using UnityEngine;

public partial struct TransformAction
{
    public static TransformData SineMove(TransformData startData, float speed, float time)
    {
        var SineUpDown = new Vector3(0, Mathf.Sin(time) * speed, 0);
        var forward = startData.Rotation * Vector3.forward * (speed * time);

        startData.Position = startData.Position + forward + SineUpDown;

        return startData;
    }
}
