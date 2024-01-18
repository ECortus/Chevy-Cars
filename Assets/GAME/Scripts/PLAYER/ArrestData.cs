using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrestData : MonoBehaviour
{
    PlayerController Player => PlayerController.Instance;
    
    [SerializeField] private PlayerSettings settings;
    private int attemptToLose => settings.ReviveAttempt;
    public static int Attempt { get; set; }
    
    public void SetFree()
    {
        Player.On(Player.Transform.position);
    }
    
    public void GetArrested()
    {
        Player.Stop();
        
        if (Attempt < attemptToLose)
        {
            ReviveOperator.Instance.On();
            Attempt++;
        }
        else
        {
            Busted();
        }
    }

    public void Busted()
    {
        LevelManager.Instance.Lose();
    }
}
