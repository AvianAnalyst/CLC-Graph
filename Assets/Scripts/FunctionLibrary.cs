using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionName
    {
        Wave,
        MultiWave,
        Ripple
    }

    private static Function[] functions = { Wave, MultiWave, Ripple };

    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }

    public static Vector3 Wave(float u, float v, float t)
    {
        return new Vector3(u, Sin(PI * (u + v + t)), v);
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        var y = Sin(PI * (u + t * .5f));
        y += .5f * Sin(2f * (PI * (v + t)));
        y += Sin(PI * (u + v + .25f * t));
        return new Vector3(u, y * (1f / 2.5f), v);
    }

    public static Vector3 Ripple(float u, float v, float t)
    {
        var d = Sqrt(u * u + v * v);
        return new Vector3(u, Sin(PI * (4f * d - t)) / (1 + d * 10), v);
    }
}