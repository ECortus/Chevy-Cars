using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QualitySetting : MonoBehaviour
{
    // [SerializeField] private TMP_Dropdown Dropdown;

    [SerializeField] private Toggle low, middle, high;
    
    private int Index
    {
        get => PlayerPrefs.GetInt("QualitySettingIndex", 0);
        set
        {
            PlayerPrefs.SetInt("QualitySettingIndex", value);
            PlayerPrefs.Save();
        }
    }
    
    private void Awake()
    {
        // Dropdown.value = Index;

        SetQuality(Index);
        
        low.isOn = Index == 0;
        middle.isOn = Index == 1;
        high.isOn = Index == 2;
    }
    
    public void SetQuality(int index)
    {
        Index = index;
        switch (Index)
        {
            case 0:
                if (QualitySettings.GetQualityLevel() != 0)
                {
                    QualitySettings.SetQualityLevel(0, true);
                }
                break;
            case 1:
                if (QualitySettings.GetQualityLevel() != 1)
                {
                    QualitySettings.SetQualityLevel(1, true);
                }
                break;
            case 2:
                if (QualitySettings.GetQualityLevel() != 2)
                {
                    QualitySettings.SetQualityLevel(2, true);
                }
                break;
            default:
                break;
        }
    }
}
