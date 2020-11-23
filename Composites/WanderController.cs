using Godot;

namespace ArthurProject.Composites
{
    public sealed class WanderController : Node2D
    {
        [Export] private int _wanderRange = 24;
        private Vector2 _startPosition;
        internal Vector2 TargetPosition { get; set; }
        private Timer _timer { get; set; }

        public override void _Ready()
        {
            _timer = GetNode<Timer>("Timer");
            _startPosition = GlobalPosition;
            TargetPosition = GlobalPosition;
            UpdateTargetPosition();
        }

        internal void StartWanderTimer(float duration)
        {
            _timer.Start(duration);
        }
        
        internal void StopWanderTimer()
        {
            _timer.Stop();
        }

        internal float GetTimeLeft()
        {
            return _timer.TimeLeft;
        }

        private void OnTimerTimeout()
        {
            UpdateTargetPosition();
        }

        private void UpdateTargetPosition()
        {
            TargetPosition = _startPosition + CreateRandomWanderVector();
        }

        private Vector2 CreateRandomWanderVector()
        {
            var rng = new RandomNumberGenerator();
            rng.Randomize();
            var rngX = rng.RandfRange(-_wanderRange, _wanderRange);
            var rngY = rng.RandfRange(-_wanderRange, _wanderRange);
            GD.Print($"x: {rngX} -- y: {rngY}");
            return new Vector2(rngX, rngY);
        }

    }
}
