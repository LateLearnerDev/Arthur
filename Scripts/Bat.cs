using ArthurProject.Composites;
using Godot;
using ArthurProject.Scripts.Interfaces;

public class Bat : KinematicBody2D, IDestroyable
{
    private const float _slowDown = 200;
    private const float _knockbackDistance = 100;
    private Vector2 _knockback = Vector2.Zero;
    private Stats _stats;

    public override void _Ready()
    {
        _stats = GetNode<Stats>("Stats");
    }

    public override void _Process(float delta)
    {
        _knockback = _knockback.MoveToward(Vector2.Zero, _slowDown * delta);
        _knockback = MoveAndSlide(_knockback);
    }

    public void OnHurtBoxAreaEntered(Area2D area)
    {
        if (!(area is WalkingStick walkingStick)) return;
        _knockback = walkingStick.KnockbackVector * _knockbackDistance;
        _stats.Health -= walkingStick.Damage;
    }

    public void OnStatsNoHealth()
    {
        QueueFree();
    }
}
