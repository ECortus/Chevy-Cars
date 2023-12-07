using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class HelicopterController : CopBasic
{
    [Space]
    [SerializeField] private GameObject _light;

    [Space]
    [SerializeField] private Transform objectToRotate;
    [SerializeField] private float angle = 30f;

    [Space] 
    [SerializeField] private Vector3 flightOffset;
    [SerializeField] private float speedMove;

    [Space] 
    [SerializeField] private Transform screwOne;
    [SerializeField] private Transform screwTwo;

    public override Vector3 Center
    {
        get
        {
            return transform.position;
        }
    }

    private Vector3 velocity;

    public override void On(Transform target, Vector3 spawn)
    {
        FullHeal();
        
        gameObject.SetActive(true);
        SetTarget(target);

        SpawnOnStartDot(spawn + flightOffset);
        if (target)
        {
            transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized);
        }

        SetDefaultRagdoll();
        SetControl(false);
        
        _light.SetActive(true);

        RB.useGravity = false;
        RB.isKinematic = false;
    }

    public override async void Destroying()
    {
        _light.SetActive(false);
        
        base.Destroying();
        await UniTask.Delay(10000);
        Off();
    }

    public override void Off()
    {
        gameObject.SetActive(false);
    }
    
    void Start()
    {
        _health = GetComponent<HealthData>();
        _arrest = GetComponent<CopArrestController>();
        _scoreTarget = GetComponent<ScoreTarget>();
    }

    protected override void FixedUpdate()
    {
        if (!IsActive)
        {
            RB.velocity = Vector3.zero;
            ResetRotateObject();
            return;	
        }
        
        if (Died)
        {
            RB.velocity = Vector3.zero;
            ResetRotateObject();
            return;	
        }
        
        RotateScrews();
        
        if (!TakeControl && Move != Vector3.zero)
        {
            if ((Center - transform.TransformVector(flightOffset) - Target.position).magnitude > 2f)
            {
                velocity = transform.forward * speedMove;
                velocity.y = 0;
            }
            else
            {
                velocity = Vector3.zero;
            }
            
            var rotation = Quaternion.LookRotation(Move);
            transform.localRotation = (Quaternion.Slerp(
                    transform.localRotation, 
                    rotation, 
                    RotateSpeed)
                );	
        }
        else
        {
            velocity = Vector3.zero;
        }

        RB.velocity = Vector3.Lerp(
            RB.velocity,
            velocity,
            8f * Time.fixedDeltaTime
            );
        
        RotateObject();
    }

    void RotateScrews()
    {
        float power = velocity.magnitude > 0 ? 1f : 0.5f;
        screwOne.Rotate(new Vector3(0f, RotateSpeed * 400f * power, 0f), Space.Self);
        screwTwo.Rotate(new Vector3(0f, -RotateSpeed * 400f * power, 0f), Space.Self);
    }

    void RotateObject()
    {
        var angles = new Vector3(
            velocity.magnitude > 0.05f ? angle : 0f,
            0f,
            0f);

        objectToRotate.localRotation = Quaternion.RotateTowards(
            objectToRotate.localRotation, 
            Quaternion.Euler(angles), 
            27f * Time.fixedDeltaTime);
    }
    
    void ResetRotateObject()
    {
        objectToRotate.localEulerAngles = Vector3.zero;
    }
}
