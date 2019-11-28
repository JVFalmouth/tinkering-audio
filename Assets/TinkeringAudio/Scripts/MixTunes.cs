using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class MixTunes
{

    AudioClip mixedTune;

    // Update is called once per frame
    public AudioClip Mix(AudioClip clipOne, AudioClip clipTwo)
    {
        float[] clipOneSamples = new float[clipOne.samples];
        clipOne.GetData(clipOneSamples, 0);
        float[] clipTwoSamples = new float[clipTwo.samples];
        clipTwo.GetData(clipTwoSamples, 0);
        if (clipOne.samples >= clipTwo.samples)
        {
            mixedTune = combine(clipOneSamples, clipTwoSamples);
        }
        else
        {
            mixedTune = combine(clipTwoSamples, clipOneSamples);
        }
        return mixedTune;
    }

    AudioClip combine(float[] longer, float[] shorter)
    {
        float[] samples = new float[longer.Length];
        float sample;
        for (int i = 0; i < longer.Length; i++)
        {
            try
            {
                sample = longer[i] + shorter[i];
            }
            catch (System.IndexOutOfRangeException)
            {
                sample = longer[i];
            }
            samples[i] = sample;
        }
        AudioClip mixed = AudioClip.Create("mixed tune", samples.Length, 1, 44200, false);
        mixed.SetData(samples, 0);
        return mixed;
    }
}
