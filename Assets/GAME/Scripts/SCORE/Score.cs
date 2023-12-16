using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    public static Action OnUpdate;
    
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
            uint value = Value;
            
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
            for (int i = 0; i < Stages.Length; i++)
            {
                value += Stages[i].ScoreGoal;
            }

            return value;
        }
    }

    public static void Plus(uint amount)
    {
        Value += amount;
        
        if (!LevelManager.Ended) OnUpdate?.Invoke();
        
        if (ValueToCurrentGoal >= CurrentGoal)
        {
            LevelManager.Instance.ActualLevel.CompleteCurrentStage();
        }
    }
    
    public static void Minus(uint amount)
    {
        Value -= amount;
        if (!LevelManager.Ended) OnUpdate?.Invoke();
    }
    
    public static void Reset()
    {
        Value = 0;
        if (!LevelManager.Ended) OnUpdate?.Invoke();
    }
}
