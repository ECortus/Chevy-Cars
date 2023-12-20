using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DestrictionObject : MonoBehaviour
{
    private DestrictionRagdollController ragdoll;
    private ScoreTarget score;
    
    [SerializeField] private float destroyingForce = 250f;

    [Header("DEBUG")]
    public bool IsDestroyed = false;

    protected virtual void Awake()
    {
        _cancellation = new CancellationTokenSource();
        
        ragdoll = GetComponent<DestrictionRagdollController>();
        score = GetComponent<ScoreTarget>();
        
        AddToPool();
        Revive();
    }

    void AddToPool()
    {
        DestrictionPool.Instance.Add(this);
    }

    void ResetDestroyed()
    {
        DestrictionPool.Instance.ResetDestroyed(this);
    }
    
    public virtual void Revive()
    {
        IsDestroyed = false;

        gameObject.SetActive(true);
        ragdoll.SetDefault();
    }

    private CancellationTokenSource _cancellation;

    public virtual void Destroy()
    {
        IsDestroyed = true;
        ragdoll.ForceFromDot(transform.position, destroyingForce);
        
        ResetDestroyed();

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
        if (IsDestroyed || !GameManager.GameStarted) return;
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Cop"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                score.AddPoint();
            }
            
            Destroy();
        }
    }
}
