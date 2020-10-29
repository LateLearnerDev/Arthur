using Godot;

public class Grass : Node2D
{
    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionPressed("attack"))
        {
            var grassEffect = (ResourceLoader.Load("res://Entities/GrassEffect.tscn") as PackedScene)?.Instance() as Node2D;
            var world = GetTree().CurrentScene;
            world.AddChild(grassEffect);
            grassEffect.GlobalPosition = GlobalPosition;
            QueueFree();
        }
              
    }
}
