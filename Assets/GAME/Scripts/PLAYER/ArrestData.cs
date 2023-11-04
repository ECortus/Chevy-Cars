using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrestData : MonoBehaviour
{
    PlayerController Player => PlayerController.Instance;
    
    public void SetFree()
    {
        Player.On(Player.Transform.position);
    }
    
    public void GetArrested()
    {
        Player.Stop();
    }

    public void Busted()
    {
        LevelManager.Instance.Lose();
    }
}
