using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

/// <summary>
/// This is for creating tones, contains
/// - The class for controling the audio-source and setting the sound of such.
/// - A sin wave class which has the methods for generating sin wave tones.
/// </summary>

public class AudioTinker : MonoBehaviour {

    Dictionary<string,float> notes = new Notes().notes;
    public AudioSource audioSource;
    public int freq = 1500;
    public int length = 1;
    public float amp = 0.25f;
    public long startIndex = 0;
    public Slider freqSlider;
    public SineWave sineWave;
    public SquareWave squareWave;
    [Range(0.01f, 10f)]
    public float sampleLength = 1f;
    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start() {
        squareWave = new SquareWave();
        sineWave = new SineWave();
        dropdown = GameObject.FindObjectOfType<Dropdown>();
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
        if (dropdown.value == 0)
        {
            audioSource.volume = 1;
        }
        else
        {
            audioSource.volume = 1;
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
        if (dropdown.value == 0)
        {
            sineWave.MakeWave(freq, sampleLength);
            SetAudioSourceClip(sineWave.clip);
        }
        else
        {
            squareWave.MakeWave(freq, sampleLength);
            SetAudioSourceClip(squareWave.clip);
        }
    }

    public AudioClip MakeWave(int frequency, float noteLength)
    {
        if (dropdown.value == 0)
        {
            sineWave.MakeWave(frequency, noteLength);
            return sineWave.clip;
        }
        else
        {
            squareWave.MakeWave(frequency, noteLength);
            return squareWave.clip;
        }
    }

    public void SaveWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        AudioClip audioClip;
        if (dropdown.value == 0)
        {
            audioClip = sineWave.clip;
        }
        else
        {
            audioClip = squareWave.clip;
        }
        SaveWavUtil.Save(path, audioClip);
    }

    internal void MakeWave(float v)
    {
        throw new NotImplementedException();
    }
}