using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareWave
{
    protected float freq;
    protected float amp = 0.1f;
    public AudioClip clip;

    public SquareWave(int frequency = 1)
    {
        freq = frequency;
        MakeWave((int)freq, 0.5f);
    }

    // Generates the tone of the provided frequency with a square wave.
    public void MakeWave(int frequency, float sampleDuration)
    {
        int sampleRate = 44200;
        int sampleLength = (int)(sampleRate * sampleDuration);
        float maxValue = amp;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float sample = Mathf.Sin(2f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = sample * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        clip = audioClip;
    }
}