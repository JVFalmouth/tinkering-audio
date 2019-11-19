using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class AudioTinker : MonoBehaviour {
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public int freq = 1500;
    public int length = 1;
    public float amp = 0.25f;
    
    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        SinWav Wave = new SinWav(freq);
        audioSource.clip = Wave.clip;
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
            SinWav Wave = new SinWav(freq);
            audioSource.clip = Wave.clip;
        }
        if (Input.GetKeyDown("down"))
        {
            freq -= 100;
            SinWav Wave = new SinWav(freq);
            audioSource.clip = Wave.clip;
        }
        if (Input.GetKeyDown("right"))
        {
            length++;
            SinWav Wave = new SinWav(freq);
            audioSource.clip = Wave.clip;
        }
        if (Input.GetKeyDown("left"))
        {
            length--;
            SinWav Wave = new SinWav(freq);
            audioSource.clip = Wave.clip;
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
}

class SquareWav
{
    float freq = 1000f;
    float amp = 0.1f;
    public AudioClip clip;

    // Start is called before the first frame update
    public SquareWav(float frequency)
    {
        freq = frequency;
        clip = MakeSquareWave(freq);
    }

    private AudioClip MakeSquareWave(float frequency)
    {
        int sampleDurationSecs = 1;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = amp;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = 0; i < sampleLength; i++)
        {
            float sample = Mathf.Sin(Mathf.Sin(2 * Mathf.PI * freq * Time.deltaTime));
            float v = sample * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}

class SinWav
{
    float freq = 1000f;
    float amp = 0.1f;
    public AudioClip clip;

    // Start is called before the first frame update
    public SinWav(float frequency)
    {
        freq = frequency;
        clip = MakeSinWave(freq);
    }

    private AudioClip MakeSinWave(float frequency)
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

