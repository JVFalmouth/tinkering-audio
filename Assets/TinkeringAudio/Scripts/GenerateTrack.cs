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
    int key = 0;
    int length = 10;

    // Start is called before the first frame update
    void Start()
    {
        gen = GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioTinker>();
    }

    public void GenerateTune()
    {
        audioTrack = new LinkedList<AudioClip>();
        for (int i = 0; i < length; i++)
        {
            // There are 12 notes in an octave, but random.range is upper exclusive.
            note = Random.Range(0, 13);
            int freq = (int)(440 * Mathf.Pow((1.059463f), note));

            // Creates a new frequency.
            gen.Wave.MakeWave(freq);

            // Adds the frequecny to the linked list.
            audioTrack.AddLast(gen.Wave.clip);
        }

        // This will add all of the audio tracks together.
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
        gen.audioSource.clip = tune;
        gen.audioSource.Play();
    }

    public void SaveTuneWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = tune;
        SaveWavUtil.Save(path, audioClip);
    }
}
