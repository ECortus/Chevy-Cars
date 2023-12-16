using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EndlessLevel : Level
{
    public override async void StartingLevel()
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
        
        LevelManager.Instance.SetEndlessUI();
    }

    public override void CompleteCurrentStage() { }
}
