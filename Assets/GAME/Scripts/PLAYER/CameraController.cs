using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class CameraController : Instancer<CameraController>
{
    protected override void SetInstance()
    {
        Instance = this;
    }
    
    public static Transform DefaultTarget;

    [SerializeField] private Camera cam;
    
    [Space]
    [SerializeField] private float defaultDistanceToTarget = 9f;
    [SerializeField] private float upSpace = 2f;
    [Space]
    [SerializeField] private float speedMove = 5f;
    
    private float distanceToTarget;
    private Transform target;
    
    public void SetTarget(Transform trg) => target = trg;
    public void ResetTarget() => SetTarget(DefaultTarget);
    public void SetDistance(float dist)
    {
        distanceToTarget = dist;
        if (cam.orthographic) cam.orthographicSize = distanceToTarget;
    }
    public void ResetDistance() => SetDistance(defaultDistanceToTarget);
    public void ResetPosition() => transform.position = position;
    
    public void Reset()
    {
        ResetTarget();
        ResetDistance();
        ResetPosition();
    }
    
    private Vector3 position => target.position - 
        transform.rotation * new Vector3(0,0,1) * distanceToTarget * (cam.orthographic ? 2f : 1f) + new Vector3(0f, upSpace, 0f);

    void Update()
    {
        if (target)
        {
            // transform.position = Vector3.Lerp(
            //     transform.position, position, speedMove * Time.unscaledDeltaTime
            // );
            transform.position = position;
        }
    }
}
