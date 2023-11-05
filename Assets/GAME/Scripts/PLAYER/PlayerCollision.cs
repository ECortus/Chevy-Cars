using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        CopController cop;
        GameObject go = other.gameObject;
        
        if (go.CompareTag("Cop"))
        {
            if (!go.TryGetComponent(out cop))
            {
                cop = go.GetComponentInParent<CopController>();
            }
            
            if(cop) cop.GetHit(1);
        }
    }
}
