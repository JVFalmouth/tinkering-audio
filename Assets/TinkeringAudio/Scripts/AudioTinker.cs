using System.Collections;
using System.Collections.Generic;
//using NaughtyAttributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class AudioTinker : MonoBehaviour {
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public int freq = 1500;
    public int length = 1;
    public float amp = 0.25f;
    
    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = CreateToneAudioClip(freq);
    }
    
    private AudioClip CreateToneAudioClip(int frequency) {
        int sampleDurationSecs = length;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = amp;
        
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);
        
        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++) {
            float sample = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float) i / (float) sampleRate));
            //float sample = Random.Range(-1, 1);
            float v = sample * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

    void Update()
    {
        /*if (Input.GetKeyDown("space"))
        {
            PlayOutAudio();
        }*/
        if (Input.GetKeyDown("up"))
        {
            freq += 100;
            audioSource.clip = CreateToneAudioClip(freq);
        }
        if (Input.GetKeyDown("down"))
        {
            freq -= 100;
            audioSource.clip = CreateToneAudioClip(freq);
        }
        if (Input.GetKeyDown("right"))
        {
            length++;
            audioSource.clip = CreateToneAudioClip(freq);
        }
        if (Input.GetKeyDown("left"))
        {
            length--;
            audioSource.clip = CreateToneAudioClip(freq);
        }
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space");
            audioSource.loop = true;
            audioSource.Play();
        }
        if (Input.GetKeyUp("space"))
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
        if (Input.GetKeyDown("return"))
        {
            SaveWavFile();
        }

    }

    private void squareWave()
    {

    }


#if UNITY_EDITOR
    //[Button("Save Wav file")]
    private void SaveWavFile() {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = CreateToneAudioClip(1500);
        SaveWavUtil.Save(path, audioClip);
    }
#endif
}