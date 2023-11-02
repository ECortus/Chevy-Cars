using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerCarController : CarController
{
    [Inject] public static PlayerCarController Instance { get; set; }
    [Inject] void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }
    
    private Transform _handle;
    private Vector3 Move => new Vector3(_handle.localPosition.x, 0, _handle.localPosition.y);

    void Start()
    {
        _handle = GameManager.Instance.Joystick.transform.GetChild(0);
    }
    
    protected override void FixedUpdate()
    {
        if (TakeControl)
        {
            SetMotor(0);
            return;	
        }
		
        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && Move != Vector3.zero)
        {
            SetMotor(2);
            
            var rotation = Quaternion.LookRotation(Move);
            Vector3 targetDirection = DirectionFromAngle(GameManager.Instance.MyCamera.transform.eulerAngles.y, rotation.eulerAngles.y);
            
            transform.localRotation = (Quaternion.Slerp(
                transform.localRotation, 
                Quaternion.LookRotation(targetDirection), 
                RotateSpeed)
            );
        }
        else
        {
            SetMotor(0);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
