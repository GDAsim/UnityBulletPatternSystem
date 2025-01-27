using System;
using UnityEngine;

[Serializable]
public struct TransformData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;
    public float Speed;

    public TransformData(Transform transform, float speed = 1)
    {
        transform.GetLocalPositionAndRotation(out Position, out Rotation);
        Scale = transform.localScale;

        Speed = speed;
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
        transform.localRotation = Rotation * transform.localRotation;
        transform.localScale += Scale;
    }

    public static TransformData operator -(TransformData left, TransformData right)
    {
        left.Position -= right.Position;
        left.Rotation = left.Rotation * Quaternion.Inverse(right.Rotation);
        left.Scale -= right.Scale;
        return left;
    }
}