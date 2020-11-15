using System;
using ArthurProject.Scripts.Interfaces;
using Godot;

namespace ArthurProject.Collisions
{
    public class HurtBox : Area2D, IDestroyable
    {
        private readonly PackedScene _hitEffectScene = (PackedScene)ResourceLoader.Load("res://Entities/HitEffect.tscn");
        private Timer _timer;
        private CollisionShape2D _collision;
        private bool _isInvincible;
        internal bool IsInvincible
        {
            get => _isInvincible;
            set => SetInvincible(value);
        }
        
        [Export] private bool IsShowHit { get; set; } = true;
        
        [Signal] private delegate void InvincibilityStarted();
        [Signal] private delegate void InvincibilityEnded();

        public override void _Ready()
        {
            _timer = GetNode<Timer>("Timer");
            _collision = GetNode<CollisionShape2D>("CollisionShape2D");
        }
        
        public void OnHurtBoxAreaEntered(Area2D area)
        {
            CreateHitEffect();
        }

        private void SetInvincible(bool value)
        {
            _isInvincible = value;
            EmitSignal(_isInvincible ? nameof(InvincibilityStarted) : nameof(InvincibilityEnded));
        }

        internal void CreateHitEffect()
        {
            var hitEffectInstance = _hitEffectScene?.Instance() as AnimatedSprite;
            var main = GetTree().CurrentScene;
            main.AddChild(hitEffectInstance);
            if (hitEffectInstance != null) hitEffectInstance.GlobalPosition = GlobalPosition;
        }

        private void OnTimerTimeout()
        {
            GD.Print("Timer Ended");
            IsInvincible = false;
        } 

        internal void StartInvincibility(float duration)
        {
            IsInvincible = true;
            _timer.Start(duration);
            GD.Print("Timer Started");
        }

        private void OnHurtBoxInvincibilityStarted()
        {
            /* SetDeferred is called here because we are trying to change a physics property during physics calculations.
               SetDeferred instead will set monitoring to false at the end of the current game loop. */
            SetDeferred("monitoring", false);
        }
        
        private void OnHurtBoxInvincibilityEnded()
        {
            Monitoring = true;
        }
        
        public void OnStatsNoHealth()
        {
            throw new NotImplementedException();
        }
    }
}
