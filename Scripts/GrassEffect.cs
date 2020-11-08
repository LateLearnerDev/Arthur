using ArthurProject.Extensions;
using Godot;

public class GrassEffect : Node2D
{
    private AnimatedSprite _animatedSprite;
    private int _lastFrame => _animatedSprite.Frames.GetLastFrame("Animate");
    
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _animatedSprite.Frame = 0;
        _animatedSprite.Play("Animate");
        GD.Print(_lastFrame);
    }

    private void OnAnimatedSpriteAnimationFinished()
    {
        QueueFree();
    }
    
}
