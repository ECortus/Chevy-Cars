using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopController : CarController
{
    [Space]
    public CopType Type = CopType.Nothing;
    [SerializeField] private HealthData health;
    [SerializeField] private uint scoreForDestroying = 5;

    public void FullHeal() => health.FullHeal();
    public void Heal(int hp) => health.Heal(hp);
    public void GetHit(int dmg) => health.GetHit(dmg);
    public bool Died => health.Died;
    
    public void On(Transform target, Vector3 spawn)
    {
        FullHeal();
        
        gameObject.SetActive(true);
        SetTarget(target);

        SpawnOnStartDot(spawn);
        if (target)
        {
            transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized);
        }

        SetControl(false);
    }
    
    public void SpawnOnStartDot(Vector3 spawn)
    {
        transform.position = spawn;
        ClearTrials();
    }

    public void AddPoint() => Score.Plus(scoreForDestroying);
    
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
