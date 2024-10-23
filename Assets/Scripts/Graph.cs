using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;

    [SerializeField] [Range(10, 100)] private int resolution = 10;
    [SerializeField] private FunctionLibrary.FunctionName function = 0;
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
        var position = Vector3.zero;
        var scale = Vector3.one * step;
        currentResolution = resolution;
        points = new Transform[resolution * resolution];

        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z++;
            }

            var point = points[i] = Instantiate(pointPrefab);
            position.x = (x + .5f) * step - 1f;
            position.z = (z + .5f) * step - 1f;

            point.localPosition = position;
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
        for (var i = 0; i < points.Length; i++)
        {
            var point = points[i];
            var position = point.localPosition;
            position.y = f(position.x, position.z, time);
            point.localPosition = position;
        }
    }
}