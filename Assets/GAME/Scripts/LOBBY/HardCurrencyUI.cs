using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HardCurrencyUI : FloatingCounter
{
    [Inject] public static HardCurrencyUI Instance { get; set; }
    [Inject] void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    protected override uint resource => HardCurrency.Value;
}
