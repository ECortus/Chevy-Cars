using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthData : HealthData
{
    [SerializeField] private AchievementObject achievement;
    
    private void Start()
    {
        death.AddListener(delegate { LevelManager.Instance.Win(); });
        if (achievement) death.AddListener(delegate { achievement.AddCompletedCount(); });
    }
}
