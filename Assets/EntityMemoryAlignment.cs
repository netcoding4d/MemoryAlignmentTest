using System;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Transform2D
{
    public int Rotation;
    public Vector2Int Position;
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct DynamicBody
{
    public int Layer;
    public int AngularVelocity;
    public bool IsTrigger;
    public Vector2Int Velocity;
}

[StructLayoutAttribute(LayoutKind.Sequential, Pack = 4)]
public unsafe struct EntityMemoryAlignment
{    
    public Transform2D Transform2D;
    public DynamicBody DynamicBody;
}