using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UnityEngine;

public unsafe class MemoryAlignmentTest : MonoBehaviour
{
    const int TEST_COUNT = 10000000;

    protected Contexts _contexts   = null;

    protected Stopwatch _stopwatch1 = null;
    protected Stopwatch _stopwatch2 = null;

    protected EntitiesMemoryAlignment * _entitiesMemoryAlignment = null;

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

        UnityEngine.Debug.Log("111ElapsedMilliseconds-->" + _stopwatch1.ElapsedMilliseconds);

        ////////////////////////////////////////////////////////////
        _entitiesMemoryAlignment = (EntitiesMemoryAlignment*)Marshal.AllocHGlobal(sizeof(EntitiesMemoryAlignment)).ToPointer();
        NativeZero((byte*)_entitiesMemoryAlignment, sizeof(EntitiesMemoryAlignment));

        var entityPtr = _entitiesMemoryAlignment->Goblin(0);
        entityPtr->Transform2D.Position = new Vector2Int(1, 2);
        entityPtr->Transform2D.Rotation = 90;

        entityPtr->DynamicBody.Layer = 100;
        entityPtr->DynamicBody.Velocity = new Vector2Int(3, 4);
        entityPtr->DynamicBody.AngularVelocity = 30;
        entityPtr->DynamicBody.IsTrigger = true;

        _stopwatch2.Start();
        for (int i = 0; i < TEST_COUNT; i++)
        {
            var x = entityPtr->Transform2D.Position.x;
            var y = entityPtr->Transform2D.Position.y;
            var rotation = entityPtr->Transform2D.Rotation;

            var layer           = entityPtr->DynamicBody.Layer;
            var velocity        = entityPtr->DynamicBody.Velocity;
            var angularVelocity = entityPtr->DynamicBody.AngularVelocity;
            var isTrigger       = entityPtr->DynamicBody.IsTrigger;
        }
        _stopwatch2.Stop();

        UnityEngine.Debug.Log("222ElapsedMilliseconds-->" + _stopwatch2.ElapsedMilliseconds);

        Marshal.FreeHGlobal(new IntPtr(_entitiesMemoryAlignment));
    }

    public unsafe static void NativeZero(byte* ptr, int size)
    {
        for (int num = size - 1; num >= 0; num--)
        {
            ptr[num] = 0;
        }
    }
}