using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoftCurrencyUI : FloatingCounter
{
    [Inject] public static SoftCurrencyUI Instance { get; set; }
    [Inject] void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    protected override uint resource => SoftCurrency.Value;
}
