using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave
{
    protected float freq;
    protected float amp = 0.1f;
    public AudioClip clip;

    // Start is called before the first frame update
    public SineWave(int frequency = 1)
    {
        freq = frequency;
        MakeWave((int)freq, 0.5f);
    }


    // Generates the tone of the provided frequency.
    public void MakeWave(int frequency, float sampleDuration)
    {
        int sampleRate = 44200;
        int sampleLength = (int)(sampleRate * sampleDuration);
        float maxValue = amp;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (long i = 0; i < sampleLength; i++)
        {
            float sample = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            //float sample = Random.Range(-1, 1);
            float v = sample * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        clip = audioClip;
    }
}