using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrictionObject : MonoBehaviour
{
    private readonly List<string> Tags = new List<string>() { "Player", "Cop" };
    [SerializeField] private ScoreTarget score;

    public void Revive()
    {
        gameObject.SetActive(true);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (Tags.Contains(other.tag))
        {
            if (other.tag == "Player")
            {
                score.AddPoint();
            }
            
            Destroy();
        }
    }
}
