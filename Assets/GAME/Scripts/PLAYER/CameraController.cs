using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : Instancer<CameraController>
{
    protected override void SetInstance()
    {
        Instance = this;
    }

    [SerializeField] private Transform defaultTarget;
    [Space]
    [SerializeField] private float defaultDistanceToTarget = 9f;
    [SerializeField] private float upSpace = 2f;
    [Space]
    [SerializeField] private float speedMove = 5f;
    
    [Header("DEBUG: ")]
    [SerializeField] private float distanceToTarget;
    [SerializeField] private Transform target;
    
    public void SetTarget(Transform trg) => target = trg;
    public void ResetTarget() => target = defaultTarget;
    public void SetDistance(float dist) => distanceToTarget = dist;
    public void ResetDistance() => distanceToTarget = defaultDistanceToTarget;
    public void ResetPosition() => transform.position = position;
    
    public void Reset()
    {
        ResetTarget();
        ResetDistance();
        ResetPosition();
    }
    
    private Vector3 position => target.position - transform.rotation * new Vector3(0,0,1) * distanceToTarget + new Vector3(0f, upSpace, 0f);

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(
                transform.position, position, speedMove * Time.unscaledDeltaTime
            );
        }
    }
}
