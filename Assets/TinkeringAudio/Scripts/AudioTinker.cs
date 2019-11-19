using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTinker : MonoBehaviour {

    Dictionary<string,float> notes = new Notes().notes;
    private AudioSource audioSource;
    public float freq = 1500;
    public int length = 1;
    public float amp = 0.25f;
    public long startIndex = 0;
    SinWav Wave;

    // Start is called before the first frame update
    void Start() {
        Wave = new SinWav(0, 0);
        audioSource = GetComponent<AudioSource>();
        freq = 1100f;
    }
    
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            UpdateAudio();
            audioSource.Play();
        }
    }
    void UpdateAudio()
    {
        startIndex = Wave.MakeWave(freq, startIndex);
        audioSource.clip = Wave.clip;
        audioSource.Play();
    }
}

class SinWav
{
    protected float freq = 1000f;
    protected float amp = 0.1f;
    public AudioClip clip;

    // Start is called before the first frame update
    public SinWav(float frequency, long startIndex)
    {
        freq = frequency;
        MakeWave(freq, startIndex);
    }

    public long MakeWave(float frequency, long startIndex)
    {
        int sampleDurationSecs = 1;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = amp;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (long i = startIndex; i < startIndex + sampleLength; i++)
        {
            float sample = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            //float sample = Random.Range(-1, 1);
            float v = sample * maxValue;
            samples[i-startIndex] = v;
        }

        audioClip.SetData(samples, 0);
        clip = audioClip;
        return (startIndex + sampleLength);
    }
}
/*
class SquareWav : SinWav
{
    public SquareWav(float frequency): base(frequency) 
    {
        clip = MakeWave(freq);
    }

    private AudioClip MakeWave(float frequency)
    {
        int sampleDurationSecs = 1;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = amp;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = 0; i < sampleLength; i++)
        {
            float sample = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            if (sample > 0)
            {
                sample = 1f;
            }
            else
            {
                sample = -1f;
            }
            float v = sample * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}*/