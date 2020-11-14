using ArthurProject.Enums;
using ArthurProject.Extensions;
using Godot;

namespace ArthurProject.Scripts
{
    public class Arthur : KinematicBody2D
    {
        private Vector2 _velocity = Vector2.Zero;
        private AnimationTree _animationTree;
        private AnimationNodeStateMachinePlayback _animationState;
        private PlayerState _state;
        private WalkingStick _walkingStickHitBox;
        
        [Export] private int _speed = 1500;
        
        public override void _Ready()
        {
            _animationTree = GetNode<AnimationTree>("AnimationTree");
            _animationState = (AnimationNodeStateMachinePlayback) _animationTree.Get("parameters/playback");
            _walkingStickHitBox = GetNode<WalkingStick>("HitboxPivot/WalkingStickHitBox");

            _animationTree.Active = true;
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
            Attack();
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
                SetWalkingStickKnockbackDirection(inputVector);
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

            _velocity = MoveAndSlide(_velocity * delta);

            if (Input.IsActionJustPressed("attack"))
                _state = PlayerState.Attack;
        }

        private void SetWalkingStickKnockbackDirection(Vector2 inputVector)
        {
            _walkingStickHitBox.Knockback = inputVector;
        }

        private void Attack()
        {
            
        }

        private void AttackAnimationFinish()
        {
            _state = PlayerState.Move;
        }
    }
}
