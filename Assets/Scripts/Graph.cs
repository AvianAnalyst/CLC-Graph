using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;

    [SerializeField] [Range(10, 100)] private int resolution = 10;
    [SerializeField] private FunctionLibrary.FunctionName function = 0;
    [SerializeField] [Range(0, 1)] private float lerp = 0;
    private int currentResolution;

    private Transform[] points;

    // Start is called before the first frame update
    private void Awake()
    {
        SetUpPoints();
    }

    private void SetUpPoints()
    {
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        currentResolution = resolution;
        points = new Transform[resolution * resolution];

        for (var i = 0; i < points.Length; i++)
        {
            var point = points[i] = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentResolution != resolution)
        {
            for (var i = 0; i < points.Length; i++) Destroy(points[i].gameObject);

            SetUpPoints();
        }

        var time = Time.time;
        var f = FunctionLibrary.GetFunction(function);
        var step = 2f / resolution;
        var v = 0.5f * step - 1f;
        Vector3 g;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z++;
                v = (z + .5f) * step - 1f;
            }

            var u = (x + .5f) * step - 1f;
            g.x = u;
            g.y = 0f;
            g.z = v;

            points[i].localPosition = Vector3.Lerp(g, f(u, v, time), lerp);
        }
    }
}