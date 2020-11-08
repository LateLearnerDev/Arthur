using Godot;

namespace ArthurProject.Composites
{
    public class Stats : Node
    {
        [Export] private int MaxHealth { get; set; } = 1;
        private static int _health;
        private static bool _isDead => _health <= 0;
        internal int Health
        {
            get => _health;
            set => SetHealth(value);
        }

        public override void _Ready()
        {
            _health = MaxHealth;
        }
        
        private void SetHealth(int value)
        {
            _health = value;
            if (_isDead)
                EmitSignal(nameof(NoHealth));
        }

        [Signal]
        private delegate void NoHealth();

    }
}
