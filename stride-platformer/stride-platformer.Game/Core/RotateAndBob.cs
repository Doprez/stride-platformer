using System;
using Stride.Core.Mathematics;
using Stride.Engine;

namespace Platformer;

public class RotateAndBob : SyncScript
{
    public float BobAmplitude {get;set;} = 0.005f; // adjust this value to control the height of the bobbing motion
    public float BobFrequency {get;set;} = 0.5f; // adjust this value to control the speed of the bobbing motion
    public float RotationSpeed {get;set;} = 100.0f; // adjust this value to control the speed of the rotation

    private float time = 0.0f;

    public override void Update()
    {
        float deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;

        time += deltaTime;

        // rotate the model on the Y axis
        Entity.Transform.Rotation *= Quaternion.RotationY(RotationSpeed * deltaTime * (float)Game.UpdateTime.Elapsed.TotalSeconds);

        // bob the model up and down
        float bobOffset = BobAmplitude * (float)Math.Sin(2 * Math.PI * BobFrequency * time);
        Entity.Transform.Position.Y += bobOffset;
    }
}