using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Oscillator : MonoBehaviour
{
    private float frequency;
    public float gain;
    private float increment;
    private float phase;
    private float sampleRate = 48000.0f;
    public AudioSource audioSource;

    float[] startFrequencies;
    float[] optionsFrequencies;
    float[] quitFrequencies;
    private int currentFreq;

    //Creates arrays of notes for each melody, before the the sound is generated.
    private void Start()
    {
        startFrequencies = new float[6] { 784f, 784f, 659f, 988f, 784f, 0f, };
        optionsFrequencies = new float[6] { 784f, 784f, 659f, 523f, 587f, 0f };
        quitFrequencies = new float[6] { 784f, 784f, 659f, 523f, 494f, 0f };
    }

    //Generates sin wave to make sound, from variables.
    private void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0f * Mathf.PI / sampleRate;

        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            data[i] = (gain * Mathf.Sin(phase));
            //Checks if stereo speakers are available (2 channels). If so, it sets output to both speakers.
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

    //Increments through the specified melody array, in order to play the entire melody.
    IEnumerator PlayMelody()
    {
        gain = 0.05f;
        frequency = startFrequencies[currentFreq];
        currentFreq += 1;
        //Checks current index and length of array with modulo function, in order to check if at the end of array. Will reset if it is equal to 1.
        currentFreq %= startFrequencies.Length;
        //Pauses action for 0.2 seconds, meaning the next frequency will be played 0.2 seconds after the last.
        yield return new WaitForSeconds(0.2f);
        if (currentFreq == 5)
        {
            StopCoroutine(PlayMelody());
            audioSource.Stop();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
        //Allows melody to play again after the currentFreq index has been reset.
        else
        {
            audioSource.Play();
            StartCoroutine(PlayMelody());
        }
    }

    //Increments through the specified melody array, in order to play the entire melody.
    IEnumerator OptionsMelody()
    {
        gain = 0.05f;
        frequency = optionsFrequencies[currentFreq];
        currentFreq += 1;
        //Checks current index and length of array with modulo function, in order to check if at the end of array. Will reset if it is equal to 1.
        currentFreq %= optionsFrequencies.Length;
        //Pauses action for 0.2 seconds, meaning the next frequency will be played 0.2 seconds after the last.
        yield return new WaitForSeconds(0.2f);
        if (currentFreq == 5)
        {
            StopCoroutine(OptionsMelody());
            audioSource.Stop();
        }
        //Allows melody to play again after the currentFreq index has been reset.
        else
        {
            audioSource.Play();
            StartCoroutine(OptionsMelody());
        }
    }

    //Increments through the specified melody array, in order to play the entire melody.
    IEnumerator QuitMelody()
    {
        gain = 0.05f;
        frequency = quitFrequencies[currentFreq];
        currentFreq += 1;
        //Checks current index and length of array with modulo function, in order to check if at the end of array. Will reset if it is equal to 1.
        currentFreq %= quitFrequencies.Length;
        //Pauses action for 0.2 seconds, meaning the next frequency will be played 0.2 seconds after the last.
        yield return new WaitForSeconds(0.2f);
        if (currentFreq == 5)
        {
            StopCoroutine(QuitMelody());
            audioSource.Stop();
//Quits the runtime when Quit button is presses.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
        //Allows melody to play again after the currentFreq index has been reset.
        else
        {
            audioSource.Play();
            StartCoroutine(QuitMelody());
        }
    }

    //Linked to the OnClick() functions in the Inspector.
    public void StartGame()
    {
        StartCoroutine(PlayMelody());
    }
    //Linked to the OnClick() functions in the Inspector.
    public void Options()
    {
        StartCoroutine(OptionsMelody());
    }
    //Linked to the OnClick() functions in the Inspector.
    public void QuitGame()
    {
        StartCoroutine(QuitMelody());
    }

    public void Back()
    {
        StartCoroutine(QuitMelody());
    }
}
