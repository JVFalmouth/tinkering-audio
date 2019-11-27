using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

/// <summary>
/// This is for creating tones, contains
/// - The class for controling the audio-source and setting the sound of such.
/// - A sin wave class which has the methods for generating sin wave tones.
/// </summary>
public class AudioForUI : MonoBehaviour
{

    Dictionary<string, float> notes = new Notes().notes;
    public AudioSource audioSource;
    public int freq = 1500;
    public int length = 1;
    public float amp = 0.25f;
    public long startIndex = 0;
    public Slider freqSlider;
    LinkedList<AudioClip> audioTrack = new LinkedList<AudioClip>();
    public SinWav Wave;

    // Start is called before the first frame update
    void Start()
    {
        Wave = new SinWav(0, 0);
        audioSource = GetComponent<AudioSource>();
        freq = 1100;
    }

    // Updates the frequency to be the value of the slider in the scene.
    void Update()
    {
        if (freqSlider != null)
        {
            freq = (int)(440 * Mathf.Pow((1.059463f), freqSlider.value));
        }
    }

    // Sets the audio source to have the clip provided, then plays it.
    public void SetAudioSourceClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
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

public class SinWav
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


    // Generates the tone of the provided frequency.
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
    private void btnPlay_Click()
    {
        int freq = int.Parse();
        int duration = int.Parse();

        System.Console.Beep(freq, duration);
    }
}
