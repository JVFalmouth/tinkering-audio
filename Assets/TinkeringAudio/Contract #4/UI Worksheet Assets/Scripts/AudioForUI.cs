// **Summary**
// This script is being used for Contract #4.
// Using the 'notes' dictionary/ script, a melody is played for each button press.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

//This initialises the class used for the audio generation
public class AudioForUI : MonoBehaviour
{
    Dictionary<string, float> notes = new Notes().notes;
    private AudioSource source;
    public int i = -1;
    readonly private int freq = 1500;
    readonly private int length = 1;
    readonly private float amp = 0.25f;
    readonly private long startIndex = 0;
    readonly LinkedList<AudioClip> audioTrack = new LinkedList<AudioClip>();
    private SinWav Wave;

    //Start is called before the first update
    void Start()
    {
        Wave = new SinWav(0, 0);
        source = GetComponent<AudioSource>();
        float freq = notes["A4"];
    }
    //Constantly updates the scene
    void Update()
    {
        float freq = notes["A4"];
    }
    //This defines the audio source
    private void SetAudioSourceClip(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    //This is specifying the correct sequence of frequencies to play
    public void UpdateAudio()
    {
        string[] startButton = { "C5", "E5", "G5" };
        if (i < 3)
        {
            foreach (string i in startButton)
            {
                this.i = this.i + 1;
                Wave.MakeWave((int)notes[startButton[this.i]]);
                source.clip = Wave.clip;
                source.Play();
            }
            this.i = -1;
        }
    }
}
