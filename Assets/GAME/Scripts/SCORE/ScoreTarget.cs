using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTarget : MonoBehaviour
{
    [SerializeField] private uint toAdd = 5;
    [SerializeField] private AchievementObject achievement;

    public void AddPoint()
    {
        if(achievement) achievement.AddCompletedCount();
        
        Score.Plus(toAdd);
        AttentionController.Instance.UpdateAttention();
    }
}
