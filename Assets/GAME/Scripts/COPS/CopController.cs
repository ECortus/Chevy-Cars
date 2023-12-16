using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CopController : CopBasic
{
    public override void On(Transform target, Vector3 spawn)
    {
        _diedTime = 0;
        
        gameObject.SetActive(true);
        SetTarget(target);

        SpawnOnStartDot(spawn);
        if (target)
        {
            transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized);
        }

        FullHeal();
        SetDefaultRagdoll();
        SetControl(false);
        
        carController.enabled = true;
    }

    public override async void Destroying()
    {
        base.Destroying();
        // await UniTask.Delay(10000);
        // Off();
    }

    public override void Off()
    {
        gameObject.SetActive(false);
    }
    
    void Start()
    {
        _health = GetComponent<HealthData>();
        _arrest = GetComponent<CopArrestController>();
        _scoreTarget = GetComponent<ScoreTarget>();
    }

    private float _diedTime;

    protected override void FixedUpdate()
    {
        if (!IsActive)
        {
            Stop();
            SetMotor(0);
            return;	
        }

        if (Died)
        {
            _diedTime += Time.fixedDeltaTime;
            SetMotor(0);
            
            if (_diedTime > 10f)
            {
                Off();
            }
            
            return;
        }
        
        if (TakeControl)
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
