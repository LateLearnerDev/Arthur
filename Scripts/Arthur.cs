using Godot;

namespace ArthurProject.Scripts
{
    public class Arthur : KinematicBody2D
    {
        private int Speed { get; set; } = 30;
        private Vector2 _velocity = Vector2.Zero;
        private AnimationPlayer AnimationPlayer { get; set; }
        private AnimationTree AnimationTree { get; set; }
        private AnimationNodeStateMachinePlayback _animationState;
        
        public override void _Ready()
        {
            AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            AnimationTree = GetNode<AnimationTree>("AnimationTree");
            _animationState = (AnimationNodeStateMachinePlayback) AnimationTree.Get("parameters/playback");
        }

        public override void _PhysicsProcess(float delta)
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            var inputVector = Vector2.Zero;
            inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            inputVector.SetToNormalize();

            if (inputVector != Vector2.Zero)
            {
                AnimationTree.Set("parameters/Idle/blend_position", inputVector);
                AnimationTree.Set("parameters/Walk/blend_position", inputVector);
                _animationState.Travel("Walk");
                _velocity = inputVector * Speed;
            }
            else
            {
                _animationState.Travel("Idle");
                _velocity = Vector2.Zero;
            }

            GD.Print(AnimationPlayer.CurrentAnimation);
            _velocity = MoveAndSlide(_velocity);
            
        }
    

    }
}
