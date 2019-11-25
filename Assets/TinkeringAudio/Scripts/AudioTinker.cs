using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class AudioTinker : MonoBehaviour {

    Dictionary<string,float> notes = new Notes().notes;
    private AudioSource audioSource;
    public int freq = 1500;
    public int length = 1;
    public float amp = 0.25f;
    public long startIndex = 0;
    public Slider freqSlider;
    LinkedList<AudioClip> audioTrack = new LinkedList<AudioClip>();
    SinWav Wave;

    // Start is called before the first frame update
    void Start() {
        Wave = new SinWav(0, 0);
        audioSource = GetComponent<AudioSource>();
        freq = 1100;
    }
    
    void Update()
    {
        freq = (int)(440 * Mathf.Pow((1.059463f), freqSlider.value));
    }
    public void UpdateAudio()
    {
        Wave.MakeWave(freq);
        audioSource.clip = Wave.clip;
        audioSource.Play();
    }

    public void SaveWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
         var audioClip = Wave.clip;
        SaveWavUtil.Save(path, audioClip);
    }
}

class SinWav
{
    protected int freq = 1000;
    protected float amp = 0.1f;
    public AudioClip clip;

    // Start is called before the first frame update
    public SinWav(int frequency, long startIndex)
    {
        freq = frequency;
        MakeWave(freq);
    }

    public void MakeWave(int frequency)
    {
        int sampleDurationSecs = 1;
        int sampleRate = 44200;
        int sampleLength = sampleRate * sampleDurationSecs;
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