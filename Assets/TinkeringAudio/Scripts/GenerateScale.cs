using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenerateScale : MonoBehaviour
{
    // A linked list is used in order to generate audio tracks programatically by adding to the list.
    LinkedList<AudioClip> audioTrack = new LinkedList<AudioClip>();
    AudioClip scale;
    public AudioTinker gen;
    int note;
    int key = 0;
    int[] majorScale = new int[] { 0, 2, 4, 5, 7, 9, 11, 12, 11, 9, 7, 5, 4, 2, 0 };
    int[] minorScale = new int[] { 0, 2, 3, 5, 7, 8, 11, 12, 11, 8, 7, 5, 3, 2, 0 };
    int[] currentKey;

    // Start is called before the first frame update
    void Start()
    {
        currentKey = majorScale;
        gen = GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioTinker>();
    }

    public void GenerateMajorScale()
    {
        audioTrack = new LinkedList<AudioClip>();
        for (int i = 0; i < currentKey.Length; i++)
        {
            // There are 12 notes in an octave, but random.range is upper exclusive.
            note = key + currentKey[i];
            int freq = (int)(440 * Mathf.Pow((1.059463f), note));

            // Creates a new frequency.
            gen.sineWave.MakeWave(freq, 0.1f);

            // Adds the frequecny to the linked list.
            audioTrack.AddLast(gen.sineWave.clip);
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
        scale = AudioClip.Create("scale", samples.Length, 1, 44200, false);
        scale.SetData(samples, 0);
        gen.audioSource.clip = scale;
        gen.audioSource.Play();
    }


    // Saves the scale to file when called. Not used in program at this time.
    public void SaveTuneWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = scale;
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
}