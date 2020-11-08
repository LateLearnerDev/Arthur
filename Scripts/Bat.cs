using Godot;
using ArthurProject.Scripts.Interfaces;

public class Bat : KinematicBody2D, IDestroyable
{
    private const float _slowDown = 200;
    private const float _knockbackDistance = 200;
    private Vector2 _knockback = Vector2.Zero;

    public override void _Process(float delta)
    {
        _knockback = _knockback.MoveToward(Vector2.Zero, _slowDown * delta);
        _knockback = MoveAndSlide(_knockback);
    }

    public void OnHurtBoxAreaEntered(Area2D area)
    {
        if (area is WalkingStick walkingStick)
            _knockback = walkingStick.KnockbackVector * _knockbackDistance;
    }
}
