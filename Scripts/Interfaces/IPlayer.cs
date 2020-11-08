using Godot;

namespace ArthurProject.Scripts.Interfaces
{
    internal interface IDestroyable
    {
        void OnHurtBoxAreaEntered(Area2D area);
        void OnStatsNoHealth();
    }
}