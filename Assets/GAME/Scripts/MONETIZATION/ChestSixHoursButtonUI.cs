using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestSixHoursButtonUI : MonoBehaviour
{
    private float BuyTime
    {
        get => PlayerPrefs.GetFloat("Chest6Hours_BuyTime", 0);
        set
        {
            PlayerPrefs.SetFloat("Chest6Hours_BuyTime", value);
            PlayerPrefs.Save();
        }
    }
    
    private void Awake()
    {
        Refresh();
    }
    
    float TimeInSeconds()
    {
        DateTime epochStart = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        float timestamp = (float)(DateTime.UtcNow - epochStart).TotalSeconds;

        return timestamp;
    }

    void Refresh()
    {
        float time = TimeInSeconds();
        gameObject.SetActive(time - BuyTime > 6 * 60 * 60);
    }

    public void OnButtonClick()
    {
        if (true)
        {
            // add actions
            
            BuyTime = TimeInSeconds();
            Refresh();
        }
    }
}
