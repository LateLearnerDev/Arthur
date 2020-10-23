using Godot;

namespace ArthurProject
{
    public static class Vector2Extensions
    {
        public static Vector2 SetToNormalize(ref this Vector2 vector2) => vector2 = vector2.Normalized();
        public static Vector2 Test() => Vector2.Zero;
    }
}