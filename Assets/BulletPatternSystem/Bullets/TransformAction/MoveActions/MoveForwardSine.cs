using UnityEngine;

public partial struct TransformAction
{
    public static TransformData SineMove1x(TransformData startData, float time)
    {
        var SineUpDown = new Vector3(0, Mathf.Sin(time), 0);
        var forward = startData.Rotation * Vector3.forward * time;

        startData.Position = startData.Position + forward + SineUpDown;

        return startData;
    }
}
