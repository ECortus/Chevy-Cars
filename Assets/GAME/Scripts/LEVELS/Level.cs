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
    
    public bool Ended { get; set; }

    [Space]
    [SerializeField] private Vector3 defaultPlayerPosition;
    [SerializeField] private bool OnDestrictionPool = true;

    public void On() => gameObject.SetActive(true);
    public void Off() => gameObject.SetActive(false);

    public void StartingLevel()
    {
        gameObject.SetActive(true);
        if(!PlayerController.Instance) LevelManager.Instance.PlayerSpawner.Spawn();
        
        ResetToDefault();
        CopsSpawnController.Instance.On();
        
        if(OnDestrictionPool) DestrictionPool.Instance.On();
        else DestrictionPool.Instance.Off();
        
        Ended = false;
    }
    
    public void CompleteCurrentStage()
    {
        if (!Ended)
        {
            Stages[StageIndex].OnEvent();
            _stageIndex = Mathf.Clamp(_stageIndex + 1, 0, Stages.Length - 1);
        }
    }

    public void RestartLevel()
    {
        ResetToDefault();
        StartingLevel();
    }

    public void WinLevel()
    {
        WinOperator.Instance.On();
        Ended = true;
    }
    
    public void LoseLevel()
    {
        LoseOperator.Instance.On();
        Ended = true;
    }

    void ResetToDefault()
    {
        Score.Reset();
        AttentionController.Instance.UpdateAttention();
        
        PlayerController.Instance.SpawnOnStartDot(defaultPlayerPosition);
        CameraController.Instance.Reset();
        
        CopsSpawnController.Instance.Off();
        CopsSpawnController.Instance.Clean();
        
        DestrictionPool.Instance.SpawnAll();
    }
}
