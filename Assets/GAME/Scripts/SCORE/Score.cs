using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    public static uint Value
    {
        get => (uint)PlayerPrefs.GetInt(PlayerPrefsNamesManager.ScoreKey, 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsNamesManager.ScoreKey, (int)value);
            PlayerPrefs.Save();
        }
    }

    public static void Plus(uint amount)
    {
        Value += amount;
        ScoreUI.Instance.Refresh();
    }
    
    public static void Minus(uint amount)
    {
        Value -= amount;
        ScoreUI.Instance.Refresh();
    }
    
    public static void Reset()
    {
        Value = 0;
        ScoreUI.Instance.Refresh();
    }
}
