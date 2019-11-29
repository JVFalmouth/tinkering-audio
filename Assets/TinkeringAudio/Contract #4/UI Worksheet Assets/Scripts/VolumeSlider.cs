//This script is intended for use in contract #4
//It is being used for a volume slider.
//Currently it does not work correctly.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.UIElements;

public class VolumeSlider : MonoBehaviour {

    public AudioMixer mixer;
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Value", 0.75f);
    }
    //This part actually changes the float value of the volume.
    //It still does not actually achieve this.
    public void SetLevel()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("Value", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Value", sliderValue);
    }
}
//This script is not used, it does adjust the value of the slider, however does not change the volume.