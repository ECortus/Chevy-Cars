using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HardCurrencyUI : FloatingCounter
{
    void Awake()
    {
        HardCurrency.OnUpdate += Refresh;
        Refresh();
    }
    
    private void OnDestroy()
    {
        HardCurrency.OnUpdate -= Refresh;
    }

    protected override int resource => HardCurrency.Value;
}
