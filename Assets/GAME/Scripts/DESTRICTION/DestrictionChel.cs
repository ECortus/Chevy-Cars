using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrictionChel : DestrictionObject
{
    [Space]
    [SerializeField] private Rigidbody Body;
    [SerializeField] private SphereCollider Sphere;

    [Space] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    
    [Space]
    [SerializeField] private Transform[] path;
    
    private int pathIndex = 0;
    private Transform dot => path[pathIndex];

    private Vector3 direction;
    private Quaternion rotation;

    public override void Revive()
    {
        base.Revive();
        
        pathIndex = 0;
        Body.position = dot.position;

        Sphere.enabled = true;
    }

    public override void Destroy()
    {
        base.Destroy();
        Sphere.enabled = false;
    }

    private void FixedUpdate()
    {
        if (IsDestroyed)
        {
            Body.isKinematic = true;
            return;
        }
        
        Body.isKinematic = false;
        
        direction = (dot.position - transform.position).normalized;
        direction.y = 0f;
        rotation = Quaternion.Euler(direction);
        
        Body.MoveRotation(Quaternion.Slerp(Body.rotation, rotation, rotateSpeed * Time.fixedDeltaTime));
        Body.velocity = transform.forward * moveSpeed;
    }
}
