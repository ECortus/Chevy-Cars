using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveOperator : Instancer<ReviveOperator>
{
    protected override void SetInstance()
    {
        Instance = this;
    }
    
    [SerializeField] private GameObject menu;

    public void On()
    {
        CameraController.Instance.SetDistance(11f);
            
        menu.SetActive(true);
        GameManager.Instance.SetTimeScale(0f);
    }

    public void Revive()
    {
        menu.SetActive(false);
        GameManager.Instance.SetTimeScale(1f);
        
        CopsPool.Instance.KillAllOnDistanceFromTarget(PlayerController.Instance.Transform, 10f);
        
        CameraController.Instance.ResetDistance();
        PlayerController.Instance.On(PlayerController.Instance.Transform.position);
    }

    public void Decline()
    {
        menu.SetActive(false);
        LevelManager.Instance.Lose();
    }
}
