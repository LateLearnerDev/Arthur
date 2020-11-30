using Godot;

public class Camera2D : Godot.Camera2D
{
    private Position2D TopLeft { get; set; }
    private Position2D BottomRight { get; set; }

    public override void _Ready()
    {
        TopLeft = GetNode<Position2D>("Limits/TopLeft");
        BottomRight = GetNode<Position2D>("Limits/BottomRight");

        LimitTop = (int)TopLeft.Position.y;
        LimitLeft = (int)TopLeft.Position.x;
        LimitRight = (int)BottomRight.Position.x;
        LimitBottom = (int)BottomRight.Position.y;
    }

}
