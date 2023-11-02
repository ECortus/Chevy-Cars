using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer Master;
    [SerializeField] private Slider slider;

    private string Name = "MasterSound";
    
    public void SetVolume()
    {
        Master.SetFloat("MasterVolume", Mathf.Log10(slider.value) * 20f + 10f);
        PlayerPrefs.SetFloat(Name, slider.value);
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey(Name))
        {
            slider.value = PlayerPrefs.GetFloat(Name);
        }
        
        SetVolume();
    }
}
