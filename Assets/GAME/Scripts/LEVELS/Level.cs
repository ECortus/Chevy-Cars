using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Level : MonoBehaviour
{
    public CopsRating CopsRating;
    
    public Stage[] Stages;
    private static int _stageIndex { get; set; }

    public int StageIndex => _stageIndex;
    public Stage CurrentStage => Stages[StageIndex];

    public static Vector3 DefaultSpawnPosition => LevelManager.Instance.ActualLevel.defaultPlayerPosition;

    [Space]
    public Vector3 defaultPlayerPosition;
    
    [Space]
    [SerializeField] protected bool OnDestrictionPool = true;
    [SerializeField] protected float DestrictionPoolDelay = 30f;

    public void On() => gameObject.SetActive(true);
    public void Off() => gameObject.SetActive(false);

    public virtual async void StartingLevel()
    {
        gameObject.SetActive(true);
        
        if (!PlayerController.Instance)
        {
            LevelManager.Instance.PlayerSpawner.Spawn();
            await UniTask.WaitUntil(() => PlayerController.Instance);
        }
        
        ResetToDefault();
        CopsSpawnController.Instance.On();
        
        if(OnDestrictionPool) DestrictionPool.Instance.On(DestrictionPoolDelay);
        else DestrictionPool.Instance.Off();
        
        LevelManager.Ended = false;
        
        LevelManager.Instance.SetRegularUI();
    }
    
    public virtual void CompleteCurrentStage()
    {
        if (!LevelManager.Ended)
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
        LevelManager.Ended = true;
    }
    
    public void LoseLevel()
    {
        LoseOperator.Instance.On();
        LevelManager.Ended = true;
    }

    public void ResetToDefault()
    {
        _stageIndex = 0;
        
        Score.Reset();
        AttentionController.Instance.UpdateAttention();
        
        PlayerController.Instance.On(defaultPlayerPosition);
        CameraController.Instance.Reset();
        
        CopsSpawnController.Instance.Off();
        CopsSpawnController.Instance.Clean();
        
        DestrictionPool.Instance.SpawnAll();
    }
}
