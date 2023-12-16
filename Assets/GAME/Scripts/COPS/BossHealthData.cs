using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthData : HealthData
{
    private void Start()
    {
        death.AddListener(delegate { LevelManager.Instance.Win(); });
    }
}
