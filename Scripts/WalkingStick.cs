using ArthurProject.Enums;
using ArthurProject.Scripts.ArthurProject.Enums;
using Godot;

public class WalkingStick : KinematicBody2D
{
    private Direction _state;
    private AnimationTree _animationTree;
    private AnimationNodeStateMachinePlayback _animationState;
    
    public override void _Ready()
    {
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _animationState = (AnimationNodeStateMachinePlayback) _animationTree.Get("parameters/playback");
        
        _animationTree.Active = true;
        _animationTree.Set();
    }

    public override void _Process(float delta)
    {
        if (_state == Direction.Up)
            HandleUpState();
        else if (_state == Direction.Right)
            HandleRightState();
        else if (_state == Direction.Down)
            HandleDownState();
        else if (_state == Direction.Left)
            HandleLeftState();
    }
    
    private void OnAnimatedSpriteAnimationFinished()
    {
        QueueFree();
    }

    private void HandleLeftState()
    {
        throw new System.NotImplementedException();
    }

    private void HandleDownState()
    {
        throw new System.NotImplementedException();
    }

    private void HandleRightState()
    {
        throw new System.NotImplementedException();
    }

    private void HandleUpState()
    {
        throw new System.NotImplementedException();
    }
}
