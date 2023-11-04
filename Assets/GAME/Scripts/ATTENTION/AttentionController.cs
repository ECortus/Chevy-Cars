using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionController : Instancer<AttentionController>
{
    protected override void SetInstance()
    {
        Instance = this;
    }

    private int _attention = 0;
    public void SetAttention(int value) => _attention = Mathf.Clamp(value, 0, 999);

    public int Attention => _attention;
    public int MaxAttention => LevelManager.Instance.ActualLevel.CopsRating.MaxSearchRate;

    private float PercentGoal => Score.Value / Score.AllGoal * 100f;
    
    private CopsSlot[] Cops => LevelManager.Instance.ActualLevel.CopsRating.GetSlots();

    public void UpdateAttention()
    {
        int attention = -1;
        for (int i = Cops.Length - 1; i < 0; i--)
        {
            if (PercentGoal >= Cops[i].GoalPercentToReachThisRating)
            {
                attention = i + 1;
            }
            else
            {
                break;
            }
        }
        SetAttention(attention);
    }
}
