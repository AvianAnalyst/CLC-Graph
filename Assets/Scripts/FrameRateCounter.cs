using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI display;
    [SerializeField] [Range(.1f, 2f)] private float SampleDuration = 1f;
    private int frames = 0;
    private float duration = 0, bestDuration = float.MaxValue, worstDuration;

    private void Update()
    {
        var frameDuration = Time.unscaledDeltaTime;
        frames++;
        duration += frameDuration;
        if (frameDuration < bestDuration) bestDuration = frameDuration;
        if (frameDuration > worstDuration) worstDuration = frameDuration;
        if (duration > SampleDuration)
        {
            display.SetText("FPS\n{0:0}\n{1:0}\n{2:0}", 1f / bestDuration, frames / duration, 1f / worstDuration);
            frames = 0;
            duration = 0f;
            bestDuration = float.MaxValue;
            worstDuration = 0f;
        }
    }
}