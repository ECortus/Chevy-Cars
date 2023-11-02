using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopsSpawnController : MonoBehaviour
{
    private CopsSlot Cops => LevelManager.Instance.ActualLevel.CopsRating.GetSlot(AttentionController.Instance.Attention);

    public void On()
    {
        
    }

    public void Off()
    {
        
    }

    public void Clean()
    {
        
    }
}
