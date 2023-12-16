using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DestrictionChel : DestrictionObject
{
    private readonly int _speed = Animator.StringToHash("Speed");
    
    [Space] 
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody body;
    [SerializeField] private SphereCollider sphere;

    [Space] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    
    [Space]
    [SerializeField] private MeshRenderer[] path;
    
    private int pathIndex = 0;
    private Transform dot => path[pathIndex].transform;

    private bool Arrived
    {
        get
        {
            if (dot)
            {
                Vector3 first = new Vector3(body.position.x, 0, body.position.z);
                Vector3 second = new Vector3(dot.position.x, 0, dot.position.z);
                return (second - first).magnitude <= 0.05f;
            }

            return false;
        }
    }

    private Vector3 direction, angles;
    private Quaternion rotation;

    protected override void Awake()
    {
        base.Awake();
        
        foreach (var VARIABLE in path)
        {
            VARIABLE.enabled = false;
        }
    }

    public override void Revive()
    {
        base.Revive();
        
        pathIndex = 0;
        body.position = dot.position;

        sphere.enabled = true;
        animator.enabled = true;
        body.isKinematic = false;
    }

    public override void Destroy()
    {
        sphere.enabled = false;
        animator.enabled = false;
        body.isKinematic = true;
        
        base.Destroy();
    }

    private void FixedUpdate()
    {
        if (IsDestroyed)
        {
            return;
        }

        if (Arrived)
        {
            pathIndex++;
            if (pathIndex >= path.Length)
            {
                pathIndex = 0;
            }
        }
        
        direction = (dot.position - body.position).normalized;
        direction.y = 0f;
        rotation = Quaternion.LookRotation(direction);

        body.rotation = Quaternion.Slerp(body.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
        body.velocity = body.transform.forward * moveSpeed;
        
        animator.SetFloat(_speed, body.velocity.magnitude);
    }
}
