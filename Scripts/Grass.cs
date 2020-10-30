using ArthurProject.Extensions;
using Godot;

public class Grass : Node2D
{
    public override void _Ready()
    {
        
    }

    private void CreateGrassEffect()
    {
        var grassEffect = ResourceLoader.Load("res://Entities/GrassEffect.tscn").GetNode2D();
        var world = GetTree().CurrentScene;
        world.AddChild(grassEffect);
        grassEffect.GlobalPosition = GlobalPosition;
    }

    private void OnHurtBoxAreaEntered(Area2D area)
    {
        CreateGrassEffect();
        QueueFree();
    }
}
