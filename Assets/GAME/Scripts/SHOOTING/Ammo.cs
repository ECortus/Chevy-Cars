using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Rigidbody rb;
    public AmmoType Type = AmmoType.Directly;

    [Space] 
    [SerializeField] private float speed;
    [SerializeField] private float gravityValue;

    [Space] 
    [SerializeField] private ParticleType effectType;
    [SerializeField] private ParticleSystem hitEffect;

    [Space] 
    [SerializeField] private TrailRenderer trail;

    private float g;

    public bool Active => gameObject.activeSelf;

    public void On(Vector3 spawn, Vector3 end)
    {
        transform.position = spawn;
        gameObject.SetActive(true);
        trail.Clear();
        
        rb.useGravity = false;
        rb.isKinematic = false;
        
        Vector3 direction = (end - spawn).normalized;
        
        if (Type == AmmoType.Directly)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            rb.velocity = speed * transform.forward;
        }
        else if (Type == AmmoType.Ballistic)
        {
            float distance = Vector3.Distance(spawn, end);
            
            transform.rotation = Quaternion.LookRotation(direction);
            Vector3 angles = transform.rotation.eulerAngles + new Vector3(-60f, 0f, 0f);
            transform.rotation = Quaternion.Euler(angles);

            g = gravityValue;
            g *= Mathf.Pow(speed, 2) / (distance * gravityValue);

            rb.velocity = transform.forward * speed;
        }
    }
    
    public void Off()
    {
        gameObject.SetActive(false);
        ParticlePool.Instance.Insert(effectType, hitEffect, transform.position);
    }

    void FixedUpdate()
    {
        if (Type == AmmoType.Ballistic)
        {
            rb.velocity -= new Vector3(0, g * Time.fixedDeltaTime, 0);
        }

        transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        LayerMask layer = other.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController.Instance.GetHit(1, HitType.Ammo);
            Off();
        }
        else if (layer == LayerMask.NameToLayer("Cop"))
        {
            Off();
        }
        else if (layer == LayerMask.NameToLayer("Ground"))
        {
            Off();
        }
    }
}

[Serializable]
public enum AmmoType
{
    Nothing, Directly, Ballistic
}
