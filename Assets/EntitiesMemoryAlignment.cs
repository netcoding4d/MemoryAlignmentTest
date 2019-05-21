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
public unsafe partial struct Goblin
{    
    public Transform2D Transform2D;
    public DynamicBody DynamicBody;
}

[StructLayoutAttribute(LayoutKind.Sequential, Pack = 4)]
public unsafe partial struct EntitiesMemoryAlignment
{
    private Goblin Goblin0;
    private Goblin Goblin1;
    private Goblin Goblin2;
    private Goblin Goblin3;
    private Goblin Goblin4;
    private Goblin Goblin5;
    private Goblin Goblin6;
    private Goblin Goblin7;
    private Goblin Goblin8;
    private Goblin Goblin9;

    public int GoblinSize
    {
        get
        {
            return 10;
        }
    }

    public Goblin* Goblin(Int32 index)
    {
        fixed (Goblin* p = &Goblin0)
        {
            return p + index;
        }
    }
}