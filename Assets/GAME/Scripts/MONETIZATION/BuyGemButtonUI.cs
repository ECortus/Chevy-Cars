using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyGemButtonUI : MonoBehaviour
{
    [SerializeField] private int gemCount = 20;

    public void OnButtonClick()
    {
        if (true)
        {
            HardCurrency.Plus(gemCount);
        }
    }
}
