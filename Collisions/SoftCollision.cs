using ArthurProject.Extensions;
using Godot;

namespace ArthurProject.Collisions
{
    public class SoftCollision : Area2D
    {
        internal bool isColliding => GetOverlappingAreas().Count > 0;

        [Export] internal float Distance { get; set; } = 50;

        internal Vector2 GetPushVector()
        {
            var areas = GetOverlappingAreas();
            var pushVector = Vector2.Zero;
            if (!isColliding) return pushVector;
        
            var area = (Area2D)areas[0];
            pushVector = GetNormalizedVectorBetweenOverlappingArea(area);
            return pushVector;
        }

        private Vector2 GetNormalizedVectorBetweenOverlappingArea(Node2D area)
        {
            var pushVector = area.GlobalPosition.DirectionTo(GlobalPosition);
            pushVector.SetToNormalize();
            return pushVector;
        }
    }
}
