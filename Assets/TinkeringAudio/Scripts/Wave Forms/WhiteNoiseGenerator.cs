using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WhiteNoiseGenerator : MonoBehaviour
{
    // Local reference to the audio controller. 
    AudioTinker audioTinker;

    void Start()
    {
        audioTinker = GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioTinker>();
    }

    // This function is responsable for setting the audiosource to play the white noise.
    public void SetWhiteNoise()
    {
        var whiteNoise = new WhiteNoise();
        whiteNoise.MakeWhiteNoise();
        audioTinker.SetAudioSourceClip(whiteNoise.clip);
    }

}

public class WhiteNoise
{
    public AudioClip clip;
    float amp = 0.1f;

    private void Start()
    {
        MakeWhiteNoise();
    }

    public void MakeWhiteNoise()
    {
        int sampleDurationSecs = 1;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = amp;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float sample = Random.Range(-1, 1);
            float v = sample * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        clip = audioClip;
    }
}