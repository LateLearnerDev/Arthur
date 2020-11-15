using Godot;

public class Dev : Node2D
{
    private readonly PackedScene _arthur = (PackedScene)ResourceLoader.Load("res://Entities/Arthur.tscn");
    
    public override void _Process(float delta)
    {
        if (!Input.IsActionJustReleased("spawn_player")) return;
        GD.Print("DEV HERE");
        var arthur = _arthur?.Instance() as KinematicBody2D;
        var ySort = GetNode<YSort>("YSort");
        ySort.AddChild(arthur);
        if (arthur != null) arthur.GlobalPosition = new Vector2(64, 32);

    }
}
