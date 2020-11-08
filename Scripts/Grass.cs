using ArthurProject.Extensions;
using Godot;

public class Grass : Node2D
{
    private void CreateGrassEffect()
    {
        var grassEffect = ResourceLoader.Load("res://Entities/GrassEffect.tscn").GetNode2D();
        var world = GetTree().CurrentScene;
        world.AddChild(grassEffect);
        grassEffect.GlobalPosition = GlobalPosition;
    }

    public void OnHurtBoxAreaEntered(Area2D area)
    {
        CreateGrassEffect();
        QueueFree();
    }

}
