using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Text textGlobalVolumeValue;
    public Text textSFXVolumeValue;

    public Slider sliderGlobalVolume;
    public Slider sliderSFXVolume;

    // Start is called before the first frame update
    void Start()
    {
        sliderGlobalVolume.onValueChanged.AddListener(updateGlobalVolumeText);
        sliderSFXVolume.onValueChanged.AddListener(updateSFXText);
    }

    public void updateGlobalVolumeText(float value)
    {
        updateSliderText(textGlobalVolumeValue, value);
    }

    public void updateSFXText(float value)
    {
        updateSliderText(textSFXVolumeValue, value);
    }

    void updateSliderText(Text sliderText, float value)
    {
        sliderText.text = String.Format("{0}%", Math.Round(value * 100f, MidpointRounding.ToEven).ToString());
    }


}
