using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : CarController
{
    [Inject] public static PlayerController Instance { get; set; }
    [Inject] void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    public void On(Vector3 spawn)
    {
        FullHeal();
        
        gameObject.SetActive(true);
        SpawnOnStartDot(spawn);
        SetControl(false);
    }
    
    public void SpawnOnStartDot(Vector3 spawn)
    {
        transform.position = spawn;
        transform.localEulerAngles = Vector3.zero;
        
        ClearTrials();
    }
    
    public void Off()
    {
        gameObject.SetActive(false);
    }
    
    public Transform Transform => transform;

    [Space] 
    [SerializeField] private HealthData health;
    [SerializeField] private ArrestData arrest;
    
    public void FullHeal() => health.FullHeal();
    public void Heal(int hp) => health.Heal(hp);
    public void GetHit(int dmg) => health.GetHit(dmg);

    public void SetFree() => arrest.SetFree();
    public void GetArrested() => arrest.GetArrested();
    public void Busted() => arrest.Busted();
    
    public bool Died => health.Died;
    
    private Transform _handle;
    private Vector3 Move => new Vector3(_handle.localPosition.x, 0, _handle.localPosition.y);

    void Start()
    {
        FullHeal();
        _handle = GameManager.Instance.Joystick.transform.GetChild(0);
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
		
        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && Move != Vector3.zero)
        {
            SetMotor(2);
            
            var rotation = Quaternion.LookRotation(Move);
            Vector3 targetDirection = DirectionFromAngle(CameraController.Instance.transform.eulerAngles.y, rotation.eulerAngles.y);
            
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
