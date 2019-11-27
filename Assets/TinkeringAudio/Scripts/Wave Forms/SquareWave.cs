using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SquareWave
{
    float freq;
    float amp = 0.1f;
    public AudioClip clip;

    public SquareWave(int frequency)
    {
        freq = frequency;
        MakeWave((int)freq, 0.1f);
    }

    // Generates the tone of the provided frequency with a square wave.
    private void MakeWave(int frequency, float sampleDuration)
    {
        int sampleRate = 44100;
        int sampleLength = (int)(sampleRate * sampleDuration);
        float maxValue = amp;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float sample = Mathf.Sin(Mathf.Sin(2 * Mathf.PI * frequency * Time.deltaTime));
            float v = sample * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        clip = audioClip;
    }
}