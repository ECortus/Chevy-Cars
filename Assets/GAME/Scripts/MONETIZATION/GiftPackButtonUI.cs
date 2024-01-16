using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPackButtonUI : MonoBehaviour
{
    private bool Buyed
    {
        get => PlayerPrefs.GetInt($"{gameObject.name}_GiftBuyed", 0) > 0;
        set
        {
            PlayerPrefs.SetInt($"{gameObject.name}_GiftBuyed", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    [SerializeField] private Prize[] prizes;
    [SerializeField] private int gemCount = 60;

    private void Start()
    {
        Refresh();
    }

    public void OnButtonClick()
    {
        if (true)
        {
            foreach (var VARIABLE in prizes)
            {
                if (VARIABLE.AnimationType == ThrowingSpriteAnimationType.Hard)
                {
                    VARIABLE.Number = gemCount;
                }
                
                VARIABLE.Get();
            }

            Buyed = true;
            Refresh();
        }
    }

    void Refresh()
    {
        gameObject.SetActive(!Buyed);
    }
}
