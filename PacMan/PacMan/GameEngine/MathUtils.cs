namespace GameEngine;

public static class MathUtils
{
    public const float PI = 3.14159265358979f;
    public const float DEG_TO_RAD = (2 * PI) / 360;
    public const float RAD_TO_DEG = 360 / (2 * PI);

    public static float Lerp(float a, float b, float t) => a * (1 - t) + b * t;
}
