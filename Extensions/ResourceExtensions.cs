using Godot;

namespace ArthurProject.Extensions
{
    public static class ResourceExtensions
    {
        public static Node2D GetNode2DInstance(this Resource resource) =>
            (resource as PackedScene)?.Instance() as Node2D;
        
        public static KinematicBody2D GetKinematicBody2DInstance(this Resource resource) =>
            (resource as PackedScene)?.Instance() as KinematicBody2D;
        
        public static AnimatedSprite GetAnimatedSpriteInstance(this Resource resource) =>
            (resource as PackedScene)?.Instance() as AnimatedSprite;
    }
}