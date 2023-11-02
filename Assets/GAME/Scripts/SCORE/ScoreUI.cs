using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreUI : DoubleBarUI
{
    [Inject] public static ScoreUI Instance { get; set; }
    [Inject] void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    protected override float Amount { get; }
    protected override float MaxAmount { get; }
}
