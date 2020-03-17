using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public float frequency = 440.0f;
    public float gain;
    private float increment;
    private float phase;
    private float sampleRate = 48000.0f;

    float[] frequencies;
    public int currentFreq;

    private void Start()
    {
        frequencies = new float[5];
        frequencies[0] = 440.0f;
        frequencies[1] = 494.0f;
        frequencies[2] = 554.0f;
        frequencies[3] = 587.0f;
        frequencies[4] = 659.0f; ;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gain = 0.05f;
            frequency = frequencies[currentFreq];
            currentFreq += 1;
            currentFreq %= frequencies.Length;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            gain = 0.0f;
        }
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
}
