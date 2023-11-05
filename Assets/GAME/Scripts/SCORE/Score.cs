using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    public static uint Value
    {
        get => (uint)PlayerPrefs.GetInt(PlayerPrefsNamesManager.ScoreKey, 0);
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsNamesManager.ScoreKey, (int)value);
            PlayerPrefs.Save();
        }
    }
    
    public static float ValueToCurrentGoal
    {
        get
        {
            uint value = Score.Value;
            
            Stage[] Stages = LevelManager.Instance.ActualLevel.Stages;
            for (int i = 0; i < LevelManager.Instance.ActualLevel.StageIndex; i++)
            {
                value -= Stages[i].ScoreGoal;
            }

            return value;
        }
    }

    public static uint CurrentGoal
    {
        get
        {
            uint goal = 0;
            goal = LevelManager.Instance.ActualLevel.CurrentStage.ScoreGoal;
            return goal;
        }
    }

    public static uint AllGoal
    {
        get
        {
            uint value = 0;
            
            Stage[] Stages = LevelManager.Instance.ActualLevel.Stages;
            for (int i = 0; i < LevelManager.Instance.ActualLevel.StageIndex + 1; i++)
            {
                value += Stages[i].ScoreGoal;
            }

            return value;
        }
    }

    public static void Plus(uint amount)
    {
        Value += amount;

        if (ValueToCurrentGoal >= CurrentGoal)
        {
            LevelManager.Instance.ActualLevel.CompleteCurrentStage();
        }
        
        ScoreUI.Instance.Refresh();
    }
    
    public static void Minus(uint amount)
    {
        Value -= amount;
        ScoreUI.Instance.Refresh();
    }
    
    public static void Reset()
    {
        Value = 0;
        ScoreUI.Instance.Refresh();
    }
}
