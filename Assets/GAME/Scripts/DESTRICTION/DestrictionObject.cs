using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DestrictionObject : MonoBehaviour
{
    private readonly List<string> Tags = new List<string>() { "Player", "Cop" };

    private DestrictionRagdollController ragdoll;
    private ScoreTarget score;

    [SerializeField] private float destroyingForce = 250f;

    public bool IsDestroyed { get; set; }

    void Start()
    {
        ragdoll = GetComponent<DestrictionRagdollController>();
        score = GetComponent<ScoreTarget>();
        
        AddToPool();
        Revive();
    }

    void AddToPool()
    {
        DestrictionPool.Instance.Add(this);
    }
    
    public void Revive()
    {
        ragdoll.SetDefault();
        
        IsDestroyed = false;
        gameObject.SetActive(true);
    }

    private CancellationTokenSource _cancellation = new CancellationTokenSource();

    public void Destroy()
    {
        ragdoll.ForceFromDot(transform.position, destroyingForce);
        
        IsDestroyed = true;

        // await UniTask.Delay(10000, DelayType.DeltaTime, PlayerLoopTiming.Update, _cancellation.Token);
        // gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _cancellation.Cancel();
    }
    
    private void OnDestroy()
    {
        _cancellation.Cancel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Tags.Contains(other.tag) && !IsDestroyed)
        {
            if (other.tag == "Player")
            {
                score.AddPoint();
            }
            
            Destroy();
        }
    }
}
