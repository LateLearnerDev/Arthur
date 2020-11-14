using ArthurProject.Collisions;
using ArthurProject.Composites;
using ArthurProject.Enums;
using ArthurProject.Extensions;
using ArthurProject.Scripts.Interfaces;
using Godot;

namespace ArthurProject.Scripts
{
    public class Bat : KinematicBody2D, IDestroyable
    {
        private readonly AnimatedSprite _enemyDeathEffect = ResourceLoader.Load("res://Entities/EnemyDeathEffect.tscn").GetAnimatedSpriteInstance();
        private const float _knockbackDistance = 100;
        private Vector2 _velocity = Vector2.Zero;
        private Vector2 _knockback = Vector2.Zero;
        private Stats _stats;
        private EnemyState _state = EnemyState.Chase;
        private PlayerDetectionZone _playerDetectionZone;
        private AnimatedSprite _animatedSprite;

        [Export] private int _acceleration { get; set; } = 30;
        [Export] private int _maxSpeed { get; set; } = 20;
        [Export] private float _friction = 200;

        public override void _Ready()
        {
            _stats = GetNode<Stats>("Stats");
            _playerDetectionZone = GetNode<PlayerDetectionZone>("PlayerDetectionZone");
            _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        }

        public override void _Process(float delta)
        {
            _knockback = _knockback.MoveToward(Vector2.Zero, _friction * delta);
            _knockback = MoveAndSlide(_knockback);

            if (_state == EnemyState.Idle)
                HandleIdleState(delta);
            else if(_state == EnemyState.Wander)
                HandleWanderState();
            else if(_state == EnemyState.Chase)
                HandleChaseState(delta);
            
            _velocity = MoveAndSlide(_velocity);
        }

        private void HandleIdleState(float delta)
        {
            SmoothMovementStop(delta);
            SeekPlayer();
        }
        
        private void HandleChaseState(float delta)
        {
            ChasePlayer(delta);
            _animatedSprite.FlipH = _velocity.x < 0;
        }
        
        private void HandleWanderState()
        {
            
        }

        private void SmoothMovementStop(float delta)
        {
            _velocity = _velocity.MoveToward(Vector2.Zero, _friction * delta);
        }
        
        private void SeekPlayer()
        {
            if (_playerDetectionZone._isAlerted)
                _state = EnemyState.Chase;
        }

        private void ChasePlayer(float delta)
        {
            var player = _playerDetectionZone.Body;
            if (player != null)
            {
                var direction = (player.GlobalPosition - GlobalPosition).Normalized();
                _velocity = _velocity.MoveToward(direction * _maxSpeed, _acceleration * delta);
            }
            else
                _state = EnemyState.Idle;
        }

        public void OnHurtBoxAreaEntered(Area2D area)
        {
            if (!(area is WalkingStick walkingStick)) return;
            GD.Print($"Damage: {walkingStick.Damage}");
            _knockback = walkingStick.Knockback * _knockbackDistance;
            _stats.Health -= walkingStick.Damage;
        }

        public void OnStatsNoHealth()
        {
            QueueFree();
            GetParent().AddChild(_enemyDeathEffect);
            _enemyDeathEffect.GlobalPosition = GlobalPosition;
        }
    }
}
