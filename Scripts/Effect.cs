using ArthurProject.Global;
using ArthurProject.Scripts.Interfaces;
using Godot;

public class Effect : AnimatedSprite, IAnimation
{
    public override void _Ready()
    {
        Connect(GodotSignals.OnAnimationFinished, this, nameof(OnAnimationFinished));
        Frame = 0;
        Play("Animate");
    }

    public void OnAnimationFinished()
    {
        QueueFree();
    }
    
}
