using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


/// <summary>
/// James Vanstone, Contract 3, melody generation.
/// Date: 26/11/2019
/// </summary>


public class GenerateTrack : MonoBehaviour
{
    // A linked list is used in order to generate audio tracks programatically by adding to the list.
    LinkedList<AudioClip> audioTrack = new LinkedList<AudioClip>();
    AudioClip tune;
    public AudioTinker gen;
    int note;
    //int key = 0; This is not used rn due to not being implemented in this script.
    // Tune length.
    int length = 10;
    [Range(0.01f,10f)]
    public float noteLength = 1f;
    MixTunes mix = new MixTunes();

    // Start is called before the first frame update
    void Start()
    {
        gen = GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioTinker>();
    }

    // This function will generate a random tune, regardless of any harmonics or key.
    public void GenerateTune()
    {
        gen.SetAudioSourceClip(makeTune()); 
    }

    // Saves the file on call.
    public void SaveTuneWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = tune;
        SaveWavUtil.Save(path, audioClip);
    }

    AudioClip makeTune()
    {
        audioTrack = new LinkedList<AudioClip>();
        for (int i = 0; i < length; i++)
        {
            // There are 12 notes in an octave, but random.range is upper exclusive.
            note = Random.Range(0, 13);
            // Sets the frequency to be an int cast of a chromatic scale starting on middle A (440Hz)
            int freq = (int)(440 * Mathf.Pow((1.059463f), note));

            // Creates a new frequency.
            var clip = gen.MakeWave(freq, noteLength);

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

    public void GenerateMixedTune()
    {
        gen.SetAudioSourceClip(mix.Mix(makeTune(), makeTune()));
    }
}
