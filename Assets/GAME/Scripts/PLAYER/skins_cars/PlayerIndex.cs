using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerIndex
{
    public static Action OnSkinUpdate { get; set; }
    public static Action OnCarUpdate { get; set; }
    
    public static int Skin
    {
        get => PlayerPrefs.GetInt("PlayerSkinIndex", 0);
        private set
        {
            PlayerPrefs.SetInt("PlayerSkinIndex", value);
            PlayerPrefs.Save();
        }
    }

    public static void SetSkin(int index)
    {
        Skin = index;
        OnSkinUpdate?.Invoke();
    }
    
    public static int Car
    {
        get => PlayerPrefs.GetInt("PlayerCarIndex", 0);
        private set
        {
            PlayerPrefs.SetInt("PlayerCarIndex", value);
            PlayerPrefs.Save();
        }
    }
    
    public static void SetCar(int index)
    {
        Car = index;
        OnCarUpdate?.Invoke();
    }
}
