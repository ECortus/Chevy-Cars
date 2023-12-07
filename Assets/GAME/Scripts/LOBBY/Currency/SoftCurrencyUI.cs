using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoftCurrencyUI : FloatingCounter
{
    void Awake()
    {
        SoftCurrency.OnUpdate += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        SoftCurrency.OnUpdate -= Refresh;
    }

    protected override int resource => SoftCurrency.Value;
}
