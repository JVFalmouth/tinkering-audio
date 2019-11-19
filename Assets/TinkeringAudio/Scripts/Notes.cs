using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Notes
{
    Dictionary<string, float> notes = new Dictionary<string, float>();
    private Notes()
    {
        notes.Add("C3", 130.81f);
        notes.Add("C#3", 138.59f);
        notes.Add("D3", 146.83f);
        notes.Add("D#3", 155.56f);
        notes.Add("E3", 164.81f);
        notes.Add("F3", 174.61f);
        notes.Add("F#3", 185.00f);
        notes.Add("G3", 196.00f);
        notes.Add("G#3", 207.65f);
        notes.Add("A3", 220.00f);
        notes.Add("A#3", 233.08f);
        notes.Add("B3", 246.94f);
        notes.Add("C4", 261.63f);
        notes.Add("C#4", 277.18f);
        notes.Add("D4", 293.66f);
        notes.Add("D#4", 311.13f);
        notes.Add("E4", 329.63f);
        notes.Add("F4", 349.23f);
        notes.Add("F#4", 369.99f);
        notes.Add("G4", 392.00f);
        notes.Add("G#4", 415.30f);
        notes.Add("A4", 440.00f);
        notes.Add("A#4", 466.16f);
        notes.Add("B4", 493.88f);
        notes.Add("C5", 523.25f);
        notes.Add("C#5", 554.37f);
        notes.Add("D5", 587.33f);
        notes.Add("D#5", 622.25f);
        notes.Add("E5", 659.25f);
        notes.Add("F5", 698.46f);
        notes.Add("F#5", 739.99f);
        notes.Add("G5", 783.99f);
        notes.Add("G#5", 830.61f);
        notes.Add("A5", 880.00f);
        notes.Add("A#5", 932.33f);
        notes.Add("B5", 987.77f);
    }
}