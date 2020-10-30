using Godot;

namespace ArthurProject.Extensions
{
    public static class ResourceExtensions
    {
        public static Node2D GetNode2D(this Resource resource) =>
            (resource as PackedScene)?.Instance() as Node2D;
        
        public static KinematicBody2D GetKinematicBody2D(this Resource resource) =>
            (resource as PackedScene)?.Instance() as KinematicBody2D;
    }
}