using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statistics
{
    public static int LevelIndex
    {
        get
        {
            int lvl = PlayerPrefs.GetInt(PlayerPrefsNamesManager.LevelIndexKey, 0);
            return lvl;
        }

        set
        {
            int lvl = value;
            PlayerPrefs.SetInt(PlayerPrefsNamesManager.LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }
}
