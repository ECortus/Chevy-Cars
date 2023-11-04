using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreUI : BarUI
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

    protected override float Amount => Score.ValueToCurrentGoal;
    protected override float MaxAmount => Score.CurrentGoal;
}
