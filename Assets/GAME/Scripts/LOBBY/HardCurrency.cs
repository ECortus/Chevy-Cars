using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HardCurrency
{
    public static uint Value
    {
        get => (uint)PlayerPrefs.GetInt(PlayerPrefsNamesManager.HardCurrencyKey, 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsNamesManager.HardCurrencyKey, (int)value);
            PlayerPrefs.Save();
        }
    }

    public static void Plus(uint amount)
    {
        Value += amount;
        HardCurrencyUI.Instance.Refresh();
    }
    
    public static void Minus(uint amount)
    {
        Value -= amount;
        HardCurrencyUI.Instance.Refresh();
    }
    
    public static void Reset()
    {
        Value = 0;
        HardCurrencyUI.Instance.Refresh();
    }
}
