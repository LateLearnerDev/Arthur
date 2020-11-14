using ArthurProject.Scripts;
using Godot;

namespace ArthurProject.Collisions
{
    public class PlayerDetectionZone : Area2D
    {
        internal KinematicBody2D Body { get; private set; }
        internal bool _isAlerted => Body != null;
    
        public override void _Ready()
        {
        
        }

        private void OnBodyEntered(Node body)
        {
            if (body is Arthur arthur)
                Body = arthur;
        }

        private void OnBodyExited(Node body)
        {
            Body = null;
        }
    }
}
