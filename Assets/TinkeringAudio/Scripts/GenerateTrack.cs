using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


/// <summary>
/// James Vanstone, Contract 3, melody generation.
/// Date: 26/11/2019
/// </summary>


public class GenerateTrack : MonoBehaviour
{

    int[] majorScale = new int[] { 0, 2, 4, 5, 7, 9, 11, 12, 11, 9, 7, 5, 4, 2, 0 };
    int[] minorScale = new int[] { 0, 2, 3, 5, 7, 8, 11, 12, 11, 8, 7, 5, 3, 2, 0 };
    int[] currentKey;

    // A linked list is used in order to generate audio tracks programatically by adding to the list.
    LinkedList<AudioClip> audioTrack = new LinkedList<AudioClip>();
    AudioClip tune;
    public AudioTinker gen;
    int note;
    //int key = 0; This is not used rn due to not being implemented in this script.
    // Tune length.
    int length = 10;
    public Slider noteLength;
    MixTunes mix = new MixTunes();

    // Start is called before the first frame update
    void Start()
    {
        currentKey = majorScale;
        gen = GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioTinker>();
    }


    // Saves the file on call.
    public void SaveTuneWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = tune;
        SaveWavUtil.Save(path, audioClip);
    }

    public void SwitchKey()
    {
        if (currentKey == majorScale)
        {
            currentKey = minorScale;
        }
        else
        {
            currentKey = majorScale;
        }
    }

    AudioClip makeTune()
    {
        audioTrack = new LinkedList<AudioClip>();
        for (int i = 0; i < length; i++)
        {
            note = currentKey[Random.Range(0, 7)];

            // Sets the frequency to be an int cast of a chromatic scale starting on middle A (440Hz)
            int freq = (int)(440 * Mathf.Pow((1.059463f), note));

            // Creates a new frequency.
            var clip = gen.MakeWave(freq, noteLength.value);

            // Adds the frequecny to the linked list.
            audioTrack.AddLast(clip);
        }

        // This will add all of the audio tracks together, end to end.
        var samples = new float[0];
        foreach (AudioClip clip in audioTrack)
        {
            float[] clipSamples = new float[clip.samples];
            clip.GetData(clipSamples, 0);
            var tempSamples = new float[samples.Length + clipSamples.Length];
            samples.CopyTo(tempSamples, 0);
            clipSamples.CopyTo(tempSamples, samples.Length);
            samples = tempSamples;
        }
        tune = AudioClip.Create("tune", samples.Length, 1, 44200, false);
        tune.SetData(samples, 0);
        return tune;
    }

    // This function will generate a random tune, in the current key. It will then play them.
    public void GenerateTune()
    {
        tune = makeTune();
        gen.SetAudioSourceClip(tune);
    }

    // This function will generate two random tunes and combine them, in the current key. It will then play them.
    public void GenerateMixedTune()
    {
        tune = mix.Mix(makeTune(), makeTune());
        gen.SetAudioSourceClip(tune);
    }
}
