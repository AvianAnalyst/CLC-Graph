using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionName
    {
        Wave,
        MultiWave,
        Ripple,
        Sphere
    }

    private static Function[] functions = { Wave, MultiWave, Ripple, Sphere };

    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }

    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        p.y = Sin(PI * (u + v + t));
        return p;
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        var y = Sin(PI * (u + t * .5f));
        y += .5f * Sin(2f * (PI * (v + t)));
        y += Sin(PI * (u + v + .25f * t));
        p.y = y;
        return p;
    }

    public static Vector3 Ripple(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        var d = Sqrt(u * u + v * v);
        p.y = Sin(PI * (4f * d - t)) / (1 + d * 10);
        return p;
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        var r = .9f + .1f * Sin(PI * (6f * u + 4f * v + t));
        var s = r * Cos(.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}