using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class AudioTinker : MonoBehaviour {
    private AudioSource audioSource;
    public int freq = 1500;
    public int length = 1;
    public float amp = 0.25f;
    
    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        SquareWav Wave = new SquareWav(1000);
        audioSource.clip = Wave.clip;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    void Update()
    {
        /*if (Input.GetKeyDown("space"))
        {
            PlayOutAudio();
        }*/
        if (Input.GetKeyDown("up"))
        {
            freq += 100;
            UpdateAudio();
            
        }
        if (Input.GetKeyDown("down"))
        {
            freq -= 100;
            UpdateAudio();
        }
        if (Input.GetKeyDown("right"))
        {
            length++;
            UpdateAudio();
        }
        if (Input.GetKeyDown("left"))
        {
            length--;
            UpdateAudio();
        }
        if (Input.GetKeyDown("space"))
        {
            audioSource.loop = true;
            audioSource.Play();
        }
        if (Input.GetKeyUp("space"))
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
    void UpdateAudio()
    {
        SinWav Wave = new SinWav(freq);
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
    public SinWav(float frequency)
    {
        freq = frequency;
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
            //float sample = Random.Range(-1, 1);
            float v = sample * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}

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
}