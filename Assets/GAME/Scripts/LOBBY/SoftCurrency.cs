using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoftCurrency
{
    public static uint Value
    {
        get => (uint)PlayerPrefs.GetInt(PlayerPrefsNamesManager.SoftCurrencyKey, 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsNamesManager.SoftCurrencyKey, (int)value);
            PlayerPrefs.Save();
        }
    }

    public static void Plus(uint amount)
    {
        Value += amount;
        SoftCurrencyUI.Instance.Refresh();
    }
    
    public static void Minus(uint amount)
    {
        Value -= amount;
        SoftCurrencyUI.Instance.Refresh();
    }
    
    public static void Reset()
    {
        Value = 0;
        SoftCurrencyUI.Instance.Refresh();
    }
}
