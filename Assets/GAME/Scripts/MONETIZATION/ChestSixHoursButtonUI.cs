using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChestSixHoursButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private Button button;

    [Space] 
    [SerializeField] private int min;
    [SerializeField] private int max;
    
    private float BuyTime
    {
        get => PlayerPrefs.GetFloat("Chest6Hours_BuyTime", 0);
        set
        {
            PlayerPrefs.SetFloat("Chest6Hours_BuyTime", value);
            PlayerPrefs.Save();
        }
    }

    private int BuyCount
    {
        get => PlayerPrefs.GetInt("Chest6Hours_BuyCount", 0);
        set
        {
            PlayerPrefs.SetInt("Chest6Hours_BuyCount", value);
            PlayerPrefs.Save();
        }
    }
    
    private void Awake()
    {
        time = TimeInSeconds();
        if (time - BuyTime > 6 * 60 * 60)
        {
            BuyCount = 6;
            BuyTime = time;
        }
        
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

        if (BuyCount > 0)
        {
            timer.gameObject.SetActive(false);
            button.interactable = true;
        }
        else
        {
            timer.gameObject.SetActive(true);
            button.interactable = false;
        }
    }

    private float time;
    private string seconds, minutes, hours;

    void Update()
    {
        if (timer.gameObject.activeSelf)
        {
            time = (BuyTime + 6 * 60 * 60) - TimeInSeconds();
            seconds = $"{(int)time % 60}";
            minutes = $"{(int)time / 60}";
            hours = $"{(int)time / 3600}";
            
            seconds += seconds.Length < 2 ? "0" : "";
            minutes += minutes.Length < 2 ? "0" : "";
            hours += hours.Length < 2 ? "0" : "";
            
            timer.text = $"{hours}:{minutes}:{seconds}";
        }
    }

    public void OnButtonClick()
    {
        if (BuyCount <= 0) return;
        
        if (true) // ad
        {
            TypedCurrency.Currency type;
            int index = Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    type = TypedCurrency.Currency.NormalPuzzle;
                    break;
                case 1:
                    type = TypedCurrency.Currency.RarePuzzle;
                    break;
                case 2:
                    type = TypedCurrency.Currency.UniquePuzzle;
                    break;
                default:
                    type = TypedCurrency.Currency.NormalPuzzle;
                    break;
            }
            
            TypedCurrency.Plus(type, Random.Range(min, max + 1));
            
            BuyTime = TimeInSeconds();
            Refresh();
        }
    }
}
