using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CopController : CopBasic
{
    public override void On(Transform target, Vector3 spawn)
    {
        FullHeal();
        
        gameObject.SetActive(true);
        SetTarget(target);

        SpawnOnStartDot(spawn);
        if (target)
        {
            transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized);
        }

        SetDefaultRagdoll();
        SetControl(false);
    }

    public override async void Destroying()
    {
        base.Destroying();
        await UniTask.Delay(10000);
        Off();
    }

    public override void Off()
    {
        gameObject.SetActive(false);
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
