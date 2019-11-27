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

public class AudioTinker : MonoBehaviour {

    Dictionary<string,float> notes = new Notes().notes;
    public AudioSource audioSource;
    public int freq = 1500;
    public int length = 1;
    public float amp = 0.25f;
    public long startIndex = 0;
    public Slider freqSlider;
    LinkedList<AudioClip> audioTrack = new LinkedList<AudioClip>();
    public SineWave Wave;
    [Range(0.01f, 10f)]
    public float sampleLength = 1f;

    // Start is called before the first frame update
    void Start() {
        Wave = new SineWave();
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
        Wave.MakeWave(freq, sampleLength);
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