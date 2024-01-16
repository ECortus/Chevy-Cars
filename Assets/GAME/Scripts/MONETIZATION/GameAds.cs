using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameAds
{
    public static bool NoAds
    {
        get => PlayerPrefs.GetInt("Game_NoAds", 0) > 0;
        private set
        {
            PlayerPrefs.SetInt("Game_NoAds", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static void SetNoAds(bool state) => NoAds = state;
}
