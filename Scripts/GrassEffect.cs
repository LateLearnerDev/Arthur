using Godot;

public class GrassEffect : Node2D
{
    private AnimatedSprite _animatedSprite;
    private int _lastFrame => _animatedSprite.Frames.GetFrameCount("Animate") - 1;
    
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _animatedSprite.Frame = 0;
        _animatedSprite.Play("Animate");
    }

    public override void _Process(float delta)
    {
        // if (Input.IsActionPressed("attack"))
        //     if (_animatedSprite.Frame == _lastFrame ||
        //         _animatedSprite.Frame == 0)
        //     {
        //         _animatedSprite.Frame = 0;
        //         _animatedSprite.Play("Animate");
        //     }
    }

    private void OnAnimatedSpriteAnimationFinished()
    {
        GD.Print("Animation finished");
    }
    
}
