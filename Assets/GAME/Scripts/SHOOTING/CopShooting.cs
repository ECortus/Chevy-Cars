using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CopShooting : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private float coolDown = 1f;
    [SerializeField] private float shootDistance = 10f;

    [Space] 
    [SerializeField] private float offsetForward = 3f;

    [Space] 
    [SerializeField] private Ammo ammo;

    private PlayerController Target => PlayerController.Instance;
    
    private Coroutine _coroutine;
    private float time;
    
    public void StartShooting()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Shooting());
            time = 0;
        }
    }

    public void StopShooting()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    
    IEnumerator Shooting()
    {
        while (true)
        {
            if ((Target.Transform.position - transform.position).magnitude <= shootDistance && time <= 0)
            {
                Shoot();
            }
            
            time -= Time.deltaTime;
            yield return null;
        }
    }
    
    private void Shoot()
    {
        time = coolDown;
        Vector3 spawn = muzzle.position;
        Vector3 end = Target.Transform.position + Target.transform.forward 
            * offsetForward * Mathf.Clamp01(Target.RB.velocity.magnitude / offsetForward);
        
        Ammo _ammo = AmmoPool.Instance.Insert(ammo.Type, ammo, spawn).GetComponent<Ammo>();
        _ammo.On(spawn, end);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }
}
