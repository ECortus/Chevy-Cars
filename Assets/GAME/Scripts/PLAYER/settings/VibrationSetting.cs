using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VibrationSetting : MonoBehaviour
{
    private GamePlayerSettings _settings;

    [SerializeField] private Toggle toggle;
    private GameObject offPart;

    private bool Mode
    {
        get => SettingsModes.Vibration;
        set => SettingsModes.Vibration = value;
    }

    private void Awake()
    {
        _settings = Resources.Load<GamePlayerSettings>("SETTINGS/PlayerSettings");
        
        offPart = toggle.targetGraphic.gameObject;
        SetVibration(Mode);
    }
    
    public void SetVibration(bool value)
    {
        Mode = value;
        toggle.isOn = Mode;
        
        if (!Mode)
        {
            //offPart.SetActive(true);
        }
        else
        {
            //offPart.SetActive(false);
        }
    }
}
