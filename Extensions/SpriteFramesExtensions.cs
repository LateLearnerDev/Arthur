using Godot;

namespace ArthurProject.Extensions
{
    public static class SpriteFramesExtensions
    {
        public static int GetLastFrame(this SpriteFrames spriteFrames, string animationName) =>
            spriteFrames.GetFrameCount(animationName) - 1;
    }
}