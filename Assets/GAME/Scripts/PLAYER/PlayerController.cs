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

    public void FullHeal() => _health.FullHeal();
    public void Heal(int hp) => _health.Heal(hp);
    public void GetHit(int dmg, HitType type) => _health.GetHit(dmg, type);

    public void On(Vector3 spawn)
    {
        CameraController.DefaultTarget = Transform;
        
        gameObject.SetActive(true);
        SpawnOnStartDot(spawn);
        
        FullHeal();
        
        SetDefaultRagdoll();
        SetControl(false);
        
        carController.enabled = true;
    }
    
    public void OnWithoutRotation(Vector3 spawn)
    {
        CameraController.DefaultTarget = Transform;
        
        gameObject.SetActive(true);
        SpawnOnStartDotWithoutRotation(spawn);
        
        FullHeal();
        
        SetDefaultRagdoll();
        SetControl(false);
        
        carController.enabled = true;
    }

    public override void Destroying()
    {
        base.Destroying();
        GetArrested();
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }
    
    public Transform Transform => transform;
    private ArrestData _arrest;

    public void SetFree() => _arrest.SetFree();
    public void GetArrested() => _arrest.GetArrested();
    public void Busted() => _arrest.Busted();
    
    private Transform _handle;
    private Vector3 Move => new Vector3(_handle.localPosition.x, 0, _handle.localPosition.y).normalized;

    private int HPBonus;
    private float SPDBonus;

    public void SetBonus(int hp, float spd)
    {
        HPBonus = hp;
        SPDBonus = spd;
    }

    void Start()
    {
        _handle ??= GameManager.Instance.Joystick.transform.GetChild(0);
        _health ??= GetComponent<HealthData>();
        _health.SetBonus(HPBonus);
        _arrest ??= GetComponent<ArrestData>();

        carController.SpeedBonus = SPDBonus;
        FullHeal();
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
