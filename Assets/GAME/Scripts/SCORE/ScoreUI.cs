using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreUI : BarUI
{
    private void OnEnable()
    {
        Score.OnUpdate += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        Score.OnUpdate -= Refresh;
    }

    protected override float Amount => Score.ValueToCurrentGoal;
    protected override float MaxAmount => Score.CurrentGoal;
}
