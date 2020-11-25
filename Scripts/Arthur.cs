using ArthurProject.Collisions;
using ArthurProject.Composites;
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
        private Stats _stats;
        private HurtBox _hurtBox;
        private AnimationPlayer _blinkAnimationPlayer;

        [Export] private int _speed = 1800;
        
        public override void _Ready()
        {
            _stats = GetNode<Stats>("PlayerStats");
            _stats.Connect("NoHealth", this, nameof(Death));
            
            _animationTree = GetNode<AnimationTree>("AnimationTree");
            _animationState = (AnimationNodeStateMachinePlayback) _animationTree.Get("parameters/playback");
            _animationTree.Active = true;
            _blinkAnimationPlayer = GetNode<AnimationPlayer>("BlinkAnimationPlayer");

            _hurtBox = GetNode<HurtBox>("HurtBox");
            
            _walkingStickHitBox = GetNode<WalkingStick>("HitboxPivot/WalkingStickHitBox");
        }

        public override void _PhysicsProcess(float delta)
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

        private void Death()
        {
            QueueFree();
        }

        private void OnInvincibilityStarted()
        {
            _blinkAnimationPlayer.Play("Start");
        }

        private void OnInvincibilityEnded()
        {
            _blinkAnimationPlayer.Play("Stop");
        }

        private void OnHurtboxAreaEntered(HitBox hitBox)
        {
            _stats.Health -= hitBox.Damage;
            _hurtBox.StartInvincibility(0.8f);
            _hurtBox.CreateHitEffect();
        }

        private void AttackAnimationFinish()
        {
            _state = PlayerState.Move;
        }
    }
}
