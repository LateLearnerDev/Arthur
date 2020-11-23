using System.Collections.Generic;
using System.Linq;
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
        private readonly RandomNumberGenerator _rng = new RandomNumberGenerator();
        private const float KnockbackDistance = 100;
        private const float WanderCheckRange = 6f;
        private Vector2 _velocity = Vector2.Zero;
        private Vector2 _knockback = Vector2.Zero;
        private Stats _stats;
        private EnemyState _state = EnemyState.Chase;
        private PlayerDetectionZone _playerDetectionZone;
        private AnimatedSprite _animatedSprite;
        private HurtBox _hurtBox;
        private SoftCollision _softCollision;
        private WanderController _wanderController;

        [Export] private int _acceleration { get; set; } = 40;
        [Export] private int _maxSpeed { get; set; } = 40;
        [Export] private float _friction = 200;

        public override void _Ready()
        {
            _stats = GetNode<Stats>("Stats");
            _hurtBox = GetNode<HurtBox>("HurtBox");
            _playerDetectionZone = GetNode<PlayerDetectionZone>("PlayerDetectionZone");
            _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            _softCollision = GetNode<SoftCollision>("SoftCollision");
            _wanderController = GetNode<WanderController>("WanderController");
        }

        public override void _PhysicsProcess(float delta)
        {
            _knockback = _knockback.MoveToward(Vector2.Zero, _friction * delta);
            _knockback = MoveAndSlide(_knockback);

            if (_state == EnemyState.Idle)
                HandleIdleState(delta);
            else if(_state == EnemyState.Wander)
                HandleWanderState(delta);
            else if(_state == EnemyState.Chase)
                HandleChaseState(delta);

            if (_softCollision.isColliding)
                _velocity += _softCollision.GetPushVector() * delta * _softCollision.Distance;
            _velocity = MoveAndSlide(_velocity);

        }
        
        private void HandleIdleState(float delta)
        {
            SmoothMovementStop(delta);
            SeekPlayer();
            SetNextStateIfWanderTimerFinished();
        }

        private void HandleWanderState(float delta)
        {
            SeekPlayer();
            SetNextStateIfWanderTimerFinished();
            AccelerateTowardsPoint(_wanderController.TargetPosition, delta);
            StopTimerIfPointReached();
        }
        
        private void HandleChaseState(float delta)
        {
            ChasePlayer(delta);
            _animatedSprite.FlipH = _velocity.x < 0;
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
        
        private void SetNextStateIfWanderTimerFinished()
        {
            if (_wanderController.GetTimeLeft() != 0) return;
            _state = GetRandomState(new List<EnemyState> {EnemyState.Idle, EnemyState.Wander});
            _rng.Randomize();
            _wanderController.StartWanderTimer(_rng.RandfRange(1, 3));
        }
        
        private void AccelerateTowardsPoint(Vector2 point, float delta)
        {
            var direction = GlobalPosition.DirectionTo(point);
            _velocity = _velocity.MoveToward(direction * _maxSpeed, _acceleration * delta);
            _animatedSprite.FlipH = _velocity.x < 0;
        }
        
        private void StopTimerIfPointReached()
        {
            if (GlobalPosition.DistanceTo(_wanderController.TargetPosition) <= WanderCheckRange)
            {
                _wanderController.StopWanderTimer();
            }
        }
        
        private void ChasePlayer(float delta)
        {
            var player = _playerDetectionZone.Body;
            if (player != null)
            {
                AccelerateTowardsPoint(player.GlobalPosition, delta);
            }
            else
                _state = EnemyState.Idle;
        }

        private EnemyState GetRandomState(IList<EnemyState> enemyStates)
        {
            return enemyStates.GetRandomItem(_rng);
        }
        
        public void OnHurtBoxAreaEntered(Area2D area)
        {
            if (!(area is WalkingStick walkingStick)) return;
            _knockback = walkingStick.Knockback * KnockbackDistance;
            _stats.Health -= walkingStick.Damage;
            _hurtBox.CreateHitEffect();
        }

        public void OnStatsNoHealth()
        {
            QueueFree();
            GetParent().AddChild(_enemyDeathEffect);
            _enemyDeathEffect.GlobalPosition = GlobalPosition;
        }
    }
}
