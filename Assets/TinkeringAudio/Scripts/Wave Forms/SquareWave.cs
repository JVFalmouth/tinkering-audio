using System.Collections;
using System.Collections.Generic;
//using NaughtyAttributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class SquareWave : MonoBehaviour
{
    private AudioSource audioSource;
    float frequency = 1000f;
    float amp = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MakeSquareWave(frequency);
        audioSource.loop = true;
        audioSource.Play();
    }

    private AudioClip MakeSquareWave(float frequency)
    {
        int sampleDurationSecs = 1;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = amp;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float sample = Mathf.Sin(Mathf.Sin((2 * Mathf.PI * Time.deltaTime) / T));
            float v = sample * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}