using Godot;

namespace ArthurProject.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 SetToNormalize(ref this Vector2 vector2) => vector2 = vector2.Normalized();
    }
}