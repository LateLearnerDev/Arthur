using Godot;

public class RemoteTransform2D : Godot.RemoteTransform2D
{
    public override void _PhysicsProcess(float delta)
    {
        GlobalPosition = new Vector2(88, GlobalPosition.y);
    }
}
