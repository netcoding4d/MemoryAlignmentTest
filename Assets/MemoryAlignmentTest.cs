using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UnityEngine;

public unsafe class MemoryAlignmentTest : MonoBehaviour
{
    const int TEST_COUNT = 1000000;

    protected Contexts _contexts   = null;

    protected Stopwatch _stopwatch1 = null;
    protected Stopwatch _stopwatch2 = null;

    void Start()
    {
        _contexts   = new Contexts();
        _stopwatch1 = new Stopwatch();
        _stopwatch2 = new Stopwatch();

        var entity = _contexts.game.CreateEntity();
        entity.AddTransform2D(new Vector2Int(1, 2), 90);
        entity.AddDynamicBody(100, new Vector2Int(3, 4), 30, true);

        _stopwatch1.Start();
        for (int i = 0; i < TEST_COUNT; i++)
        {
            var x = entity.transform2D.Position.x;
            var y = entity.transform2D.Position.y;
            var rotation = entity.transform2D.Rotation;

            var layer           = entity.dynamicBody.Layer;
            var velocity        = entity.dynamicBody.Velocity;
            var angularVelocity = entity.dynamicBody.AngularVelocity;
            var isTrigger       = entity.dynamicBody.IsTrigger;
        }
        _stopwatch1.Stop();

        UnityEngine.Debug.Log("Without MemoryAlignment ElapsedMilliseconds-->" + _stopwatch1.ElapsedMilliseconds);

        ////////////////////////////////////////////////////////////
        EntityMemoryAlignment * entityMemoryAlignment = (EntityMemoryAlignment*)Marshal.AllocHGlobal(sizeof(EntityMemoryAlignment)).ToPointer();
        ZeroMemory((byte*)entityMemoryAlignment, sizeof(EntityMemoryAlignment));

        entityMemoryAlignment->Transform2D.Position = new Vector2Int(1, 2);
        entityMemoryAlignment->Transform2D.Rotation = 90;

        entityMemoryAlignment->DynamicBody.Layer = 100;
        entityMemoryAlignment->DynamicBody.Velocity = new Vector2Int(3, 4);
        entityMemoryAlignment->DynamicBody.AngularVelocity = 30;
        entityMemoryAlignment->DynamicBody.IsTrigger = true;

        _stopwatch2.Start();
        for (int i = 0; i < TEST_COUNT; i++)
        {
            var x = entityMemoryAlignment->Transform2D.Position.x;
            var y = entityMemoryAlignment->Transform2D.Position.y;
            var rotation = entityMemoryAlignment->Transform2D.Rotation;

            var layer           = entityMemoryAlignment->DynamicBody.Layer;
            var velocity        = entityMemoryAlignment->DynamicBody.Velocity;
            var angularVelocity = entityMemoryAlignment->DynamicBody.AngularVelocity;
            var isTrigger       = entityMemoryAlignment->DynamicBody.IsTrigger;
        }
        _stopwatch2.Stop();

        UnityEngine.Debug.Log("MemoryAlignment ElapsedMilliseconds-->" + _stopwatch2.ElapsedMilliseconds);

        Marshal.FreeHGlobal(new IntPtr(entityMemoryAlignment));
    }

    public unsafe static void ZeroMemory(byte* ptr, int size)
    {
        for (int num = size - 1; num >= 0; num--)
        {
            ptr[num] = 0;
        }
    }
}