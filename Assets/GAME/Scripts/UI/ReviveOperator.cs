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
    [Space]
    [SerializeField] private PlayerSettings settings;
    private uint attemptToLose => settings.ReviveAttempt;
    private int Attempt { get; set; }

    public void On()
    {
        if (Attempt < attemptToLose)
        {
            CameraController.Instance.SetDistance(11f);
            
            menu.SetActive(true);
            GameManager.Instance.SetTimeScale(0f);
        }
        else
        {
            LevelManager.Instance.Lose();
        }
    }

    public void Revive()
    {
        menu.SetActive(false);
        GameManager.Instance.SetTimeScale(1f);
        
        CameraController.Instance.ResetDistance();
        PlayerController.Instance.On(PlayerController.Instance.Transform.position);
    }
    
    public void Lose()
    {
        menu.SetActive(false);
        LoseOperator.Instance.On();
    }
}
