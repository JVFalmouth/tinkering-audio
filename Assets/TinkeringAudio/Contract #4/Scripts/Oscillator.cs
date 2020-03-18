using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public float frequency;
    public float gain;
    private float increment;
    private float phase;
    private float sampleRate = 48000.0f;
    public AudioSource audioSource;

    float[] startFrequencies;
    float[] optionsFrequencies;
    float[] quitFrequencies;
    public int currentFreq;

    private void Start()
    {
        startFrequencies = new float[6] { 440f, 494f, 564f, 587f, 659f, 0f, };
        optionsFrequencies = new float[6] { 523f, 587f, 659f, 698f, 784, 0f };
        quitFrequencies = new float[6] { 659f, 587f, 523f, 494f, 440f, 0f };
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0f * Mathf.PI / sampleRate;

        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            data[i] = (gain * Mathf.Sin(phase));

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
            if (phase > Mathf.PI * 2)
            {
                phase = 0.0f;
            }
        }
    }

    IEnumerator PlayMelody()
    {
        gain = 0.05f;
        frequency = startFrequencies[currentFreq];
        currentFreq += 1;
        currentFreq %= startFrequencies.Length;
        yield return new WaitForSeconds(0.2f);
        if (currentFreq == 5)
        {
            StopCoroutine(PlayMelody());
            audioSource.Stop();
        } 
        else
        {
            audioSource.Play();
            StartCoroutine(PlayMelody());
        }
    }

    IEnumerator OptionsMelody()
    {
        gain = 0.05f;
        frequency = optionsFrequencies[currentFreq];
        currentFreq += 1;
        currentFreq %= optionsFrequencies.Length;
        yield return new WaitForSeconds(0.2f);
        if (currentFreq == 5)
        {
            StopCoroutine(OptionsMelody());
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
            StartCoroutine(OptionsMelody());
        }
    }

    IEnumerator QuitMelody()
    {
        gain = 0.05f;
        frequency = quitFrequencies[currentFreq];
        currentFreq += 1;
        currentFreq %= quitFrequencies.Length;
        yield return new WaitForSeconds(0.2f);
        if (currentFreq == 5)
        {
            StopCoroutine(QuitMelody());
            audioSource.Stop();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
        else
        {
            audioSource.Play();
            StartCoroutine(QuitMelody());
        }
    }

    public void StartGame()
    {
        StartCoroutine(PlayMelody());
    }

    public void Options()
    {
        StartCoroutine(OptionsMelody());
    }

    public void QuitGame()
    {
        StartCoroutine(QuitMelody());
    }
}
