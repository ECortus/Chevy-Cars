using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DavidJalbert;

public abstract class CarController : MonoBehaviour
{
	[SerializeField] protected TinyCarController carController;
	[SerializeField] private Rigidbody rb;

	[SerializeField] protected HealthData _health;
	public bool Died => _health ? _health.Died : false;

	[Space]
	[SerializeField] private TrailRenderer rightTrial;
	[SerializeField] private TrailRenderer leftTrial;
	
	[Space]
	[SerializeField] private float rotateSpeed;
	
	public Rigidbody RB => rb;
	public float RotateSpeed => rotateSpeed;

	private bool _takeControl;
	public bool TakeControl => _takeControl;
	public void SetControl(bool value) => _takeControl = value;

	public bool IsActive => gameObject.activeSelf && gameObject.activeInHierarchy;

	public float Speed => rb.velocity.magnitude;

	protected abstract void FixedUpdate();

	protected void SetMotor(uint value) => carController.setMotor(value);
	public int GetMotor() => (int)carController.getMotor();

	public virtual Vector3 Center
	{
		get => transform.position;
	}
	
	public void SpawnOnStartDot(Vector3 spawn)
	{
		Stop();

		RB.position = spawn;
		transform.position = spawn;
		
		RB.rotation = Quaternion.Euler(Vector3.zero);
		transform.rotation = Quaternion.Euler(Vector3.zero);
        
		ClearTrials();
		SetControl(false);
	}

	public void Stop()
	{
		SetControl(true);
		if(!rb.isKinematic) rb.velocity = Vector3.zero;
		if(!rb.isKinematic) rb.angularVelocity = Vector3.zero;
	}

	private void ClearTrials()
	{
		if(rightTrial) rightTrial.Clear();
		if(leftTrial) leftTrial.Clear();
	}
	
	[Space] 
	[SerializeField] private RagdollController ragdoll;
	[SerializeField] private ParticleSystem destroyingEffect;

	public void SetDefaultRagdoll()
	{
		ragdoll.SetDefault();
	}
	
	public virtual void Destroying()
	{
		carController.enabled = false;
		ragdoll.ForceFromDot(transform.position, 600f);
        
		if (destroyingEffect)
		{
			ParticlePool.Instance.Insert(ParticleType.BlowUpCar, destroyingEffect, transform.position + Vector3.up * 1.5f);
		}
	}
}