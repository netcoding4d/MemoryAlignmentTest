using Entitas;
using UnityEngine;

public sealed class Transform2DComponent : IComponent
{
    public int Rotation;
    public Vector2Int Position;
}

public sealed class DynamicBodyComponent : IComponent
{
    public int Layer;
    public int AngularVelocity;
    public bool IsTrigger;
    public Vector2Int Velocity;
}