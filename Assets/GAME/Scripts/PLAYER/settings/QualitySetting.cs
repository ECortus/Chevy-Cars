using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class QualitySetting : MonoBehaviour
{
    private TMP_Dropdown Dropdown;
    
    private int Index
    {
        get => PlayerPrefs.GetInt("QualitySettingIndex", 0);
        set
        {
            PlayerPrefs.SetInt("QualitySettingIndex", value);
            PlayerPrefs.Save();
        }
    }
    
    [Inject] void Awake()
    {
        Dropdown = GetComponent<TMP_Dropdown>();
        Dropdown.value = Index;
        SetQuality(Index);
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
