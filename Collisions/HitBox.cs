using Godot;

public class HitBox : Area2D
{
    [Export] public int Damage { get; set; } = 1;
}
