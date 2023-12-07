using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoftCurrency
{
    public static Action OnUpdate;
    
    public static int Value
    {
        get => PlayerPrefs.GetInt(PlayerPrefsNamesManager.SoftCurrencyKey, 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsNamesManager.SoftCurrencyKey, value);
            PlayerPrefs.Save();
        }
    }

    public static void Plus(int amount)
    {
        Value += amount;
        OnUpdate?.Invoke();
    }
    
    public static void Minus(int amount)
    {
        Value -= amount;
        OnUpdate?.Invoke();
    }
    
    public static void Reset()
    {
        Value = 0;
        OnUpdate?.Invoke();
    }
}
