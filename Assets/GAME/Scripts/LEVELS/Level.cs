using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public CopsRating CopsRating;
    
    public Stage[] Stages;
    private int _stageIndex = 0;

    public int StageIndex => _stageIndex;
    public Stage CurrentStage => Stages[StageIndex];

    public void StartingLevel()
    {
        ResetToDefault();
    }
    
    public void CompleteCurrentStage()
    {
        _stageIndex = Mathf.Clamp(_stageIndex + 1, 0, Stages.Length - 1);
        Stages[StageIndex].OnEvent();
    }

    public void RestartLevel()
    {
        ResetToDefault();
    }

    public void WinLevel()
    {
        WinOperator.Instance.On();
    }
    
    public void LoseLevel()
    {
        LoseOperator.Instance.On();
    }

    void ResetToDefault()
    {
        Score.Reset();
        AttentionController.Instance.UpdateAttention();
    }
}
