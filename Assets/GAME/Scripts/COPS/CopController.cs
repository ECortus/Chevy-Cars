using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CopController : CarController
{
    [Space]
    public CopType Type = CopType.Nothing;
    [SerializeField] private ScoreTarget scoreTarget;
    
    public void AddPoint() => scoreTarget.AddPoint();
    
    public async void On(Transform target, Vector3 spawn)
    {
        FullHeal();
        
        gameObject.SetActive(true);
        SetTarget(target);

        SpawnOnStartDot(spawn);
        if (target)
        {
            transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized);
        }

        await SetDefaultRagdoll();
        SetControl(false);
    }

    public override async void Destroying()
    {
        base.Destroying();
        await UniTask.Delay(10000);
        Off();
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

    private Transform _target;
    public void SetTarget(Transform trg) => _target = trg;
    public Transform Target => _target;

    private Vector3 Move
    {
        get
        {
            if (Target)
            {
                Vector3 dir = (Target.position - transform.position).normalized;
                dir.y = 0f;
                return dir;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (!IsActive)
        {
            Stop();
            SetMotor(0);
            return;	
        }
        
        if (TakeControl || Died)
        {
            SetMotor(0);
            return;	
        }
		
        if (Move != Vector3.zero)
        {
            SetMotor(2);
            
            var rotation = Quaternion.LookRotation(Move);
            
            transform.localRotation = (Quaternion.Slerp(
                    transform.localRotation, 
                    rotation, 
                    RotateSpeed)
                );
        }
        else
        {
            SetMotor(0);
        }
    }
}
