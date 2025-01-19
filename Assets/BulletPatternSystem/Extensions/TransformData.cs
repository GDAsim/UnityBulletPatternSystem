using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct TransformData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public TransformData(Transform transform)
    {
        transform.GetLocalPositionAndRotation(out Position, out Rotation);
        Scale = transform.localScale;
    }

    //public void 

    public void ApplyTo(Transform transform)
    {
        transform.localPosition = Position;
        transform.localRotation = Rotation;
        transform.localScale = Scale;
    }
    public void AddTo(Transform transform)
    {
        transform.localPosition += Position;
        transform.localRotation *= Rotation;
        transform.localScale += Scale;
    }

    public static TransformData operator -(TransformData left, TransformData right)
    {
        left.Position -= right.Position;
        left.Rotation = right.Rotation * Quaternion.Inverse(left.Rotation);
        //left.Rotation = left.Rotation * Quaternion.Inverse(right.Rotation);
        left.Scale -= right.Scale;
        return left;
    }
}