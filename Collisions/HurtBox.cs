using System;
using ArthurProject.Scripts.Interfaces;
using Godot;

namespace ArthurProject.Collisions
{
    public class HurtBox : Area2D, IDestroyable
    {
        private readonly PackedScene _hitEffectScene = (PackedScene)ResourceLoader.Load("res://Entities/HitEffect.tscn");
        [Export] private bool IsShowHit { get; set; } = true;

        public void OnHurtBoxAreaEntered(Area2D area)
        {
            if(IsShowHit)
                HandleHitEffect();
        }

        private void HandleHitEffect()
        {
            var hitEffectInstance = _hitEffectScene?.Instance() as AnimatedSprite;
            var main = GetTree().CurrentScene;
            main.AddChild(hitEffectInstance);
            if (hitEffectInstance != null) hitEffectInstance.GlobalPosition = GlobalPosition;
        }

        public void OnStatsNoHealth()
        {
            throw new NotImplementedException();
        }

    }
}
