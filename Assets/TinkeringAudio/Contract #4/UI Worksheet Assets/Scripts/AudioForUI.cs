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
    public Dictionary<string, float> notes = new Notes().notes;
    private AudioSource source;
    readonly private int freq = 1500;
    readonly private int length = 1;
    readonly private float amp = 0.25f;
    readonly private long startIndex = 0;
    readonly LinkedList<AudioClip> audioTrack = new LinkedList<AudioClip>();
    public Slider volSlider;
    private SinWav Wave;

<<<<<<< HEAD
    //Start is called before the first update
    private void Start()
    {
        Wave = new SinWav(0, 0);
        source = GetComponent<AudioSource>();
        float freq = notes["A4"];
=======
    Dictionary<string, float> notes = new Notes().notes;
    public AudioSource audioSource;
    public int freq = 1500;
    public int length = 1;
    public float amp = 0.25f;
    public long startIndex = 0;
    public Slider freqSlider;
    LinkedList<AudioClip> audioTrack = new LinkedList<AudioClip>();
    public UISinWave Wave;

    // Start is called before the first frame update
    void Start()
    {
        Wave = new UISinWave(0);
        audioSource = GetComponent<AudioSource>();
        freq = 1100;
>>>>>>> be4be8f9dda2534ab00bb6ff806a0b4d4233f5c0
    }
    //Constantly updates the scene
    void Update()
    {
        float freq = notes["A4"];
        if (volSlider != null)
        {
            float amp = volSlider.value;
        }
    }
    //This defines the audio source
    private void SetAudioSourceClip(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    //These are specifying the correct frequencies to play, 
    //which are specified by name of the note in the dictionary "Notes".
    //There is a unique function for each button pressed.
    //This is linked to each button within the inspector.
    public void UpdateAudioPlayButton()
    {
        Wave.MakeWave((int)notes["C5"]);
        source.clip = Wave.clip;
        source.Play();
    }
    public void UpdateAudioOptionsButton()
    {
        Wave.MakeWave((int)notes["A4"]);
        source.clip = Wave.clip;
        source.Play();
    }
<<<<<<< HEAD
    public void UpdateAudioQuitButton()
=======
}

public class UISinWave
{
    protected int freq = 1000;
    protected float amp = 0.1f;
    public AudioClip clip;

    // Start is called before the first frame update
    public UISinWave(int frequency)
>>>>>>> be4be8f9dda2534ab00bb6ff806a0b4d4233f5c0
    {
        Wave.MakeWave((int)notes["C#3"]);
        source.clip = Wave.clip;
        source.Play();
    }
    public void UpdateAuidoBackButton()
    {
        Wave.MakeWave((int)notes["G4"]);
        source.clip = Wave.clip;
        source.Play();
    }
    public void UpdateAudioOption1()
    {
<<<<<<< HEAD
        Wave.MakeWave((int)notes["B4"]);
        source.clip = Wave.clip;
        source.Play();
    }
    public void UpdateAudioOption2()
    {
        Wave.MakeWave((int)notes["C#5"]);
        source.clip = Wave.clip;
        source.Play();
    }
    public void UpdateAudioOption3()
    {
        Wave.MakeWave((int)notes["D#5"]);
        source.clip = Wave.clip;
        source.Play();
=======
        //int freq = int.Parse();
        //int duration = int.Parse();

        System.Console.Beep(freq, 1);
>>>>>>> be4be8f9dda2534ab00bb6ff806a0b4d4233f5c0
    }
    //^^ These are for each individual button.
}
