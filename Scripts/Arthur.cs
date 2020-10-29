using ArthurProject.Extensions;
using Godot;

namespace ArthurProject.Scripts
{
    public class Arthur : KinematicBody2D
    {
        private int _speed = 1500;
        private Vector2 _velocity = Vector2.Zero;
        private AnimationPlayer _animationPlayer;
        private AnimationTree _animationTree;
        private AnimationNodeStateMachinePlayback _animationState;
        private PlayerState _state;
        
        public override void _Ready()
        {
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            _animationTree = GetNode<AnimationTree>("AnimationTree");
            _animationState = (AnimationNodeStateMachinePlayback) _animationTree.Get("parameters/playback");

            _animationTree.Active = true;
        }

        public override void _PhysicsProcess(float delta)
        {
            
        }

        public override void _Process(float delta)
        {
            if (_state == PlayerState.Move)
                HandleMovementState(delta);
            else if (_state == PlayerState.Attack)
                HandleAttackState();
        }

        private void HandleAttackState()
        {
            _velocity = Vector2.Zero;
            _animationState.Travel("Attack");
        }

        private void HandleMovementState(float delta)
        {
            var inputVector = Vector2.Zero;
            inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            inputVector.SetToNormalize();

            if (inputVector != Vector2.Zero)
            {
                _animationTree.Set("parameters/Idle/blend_position", inputVector);
                _animationTree.Set("parameters/Walk/blend_position", inputVector);
                _animationTree.Set("parameters/Attack/blend_position", inputVector);
                _animationState.Travel("Walk");
                _velocity = inputVector * _speed;
            }
            else
            {
                _animationState.Travel("Idle");
                _velocity = Vector2.Zero;
            }

            //GD.Print(_animationPlayer.CurrentAnimation);
            _velocity = MoveAndSlide(_velocity * delta);

            if (Input.IsActionJustPressed("attack"))
                _state = PlayerState.Attack;
        }

        // Being called in Godot animation player.
        private void AttackAnimationFinish()
        {
            _state = PlayerState.Move;
        }
    }
}
