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
	[SerializeField] private float rotateSpeed;

	[Space] 
	[SerializeField] private Material trailMat;
	
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

	private TrailRenderer[] trails;

	public void SpawnOnStartDot(Vector3 spawn)
	{
		if (trails == null)
		{
			trails = GetComponentsInChildren<TrailRenderer>(true);
			foreach (var VARIABLE in trails)
			{
				VARIABLE.transform.localEulerAngles = new Vector3(0, 0, 0);
				VARIABLE.alignment = LineAlignment.View;
				VARIABLE.textureMode = LineTextureMode.Stretch;
				VARIABLE.materials = new Material[] { trailMat };
			}
		}
		
		Stop();

		RB.position = spawn;
		transform.position = spawn;
		
		// RB.rotation = Quaternion.Euler(Vector3.zero);
		// transform.rotation = Quaternion.Euler(Vector3.zero);
        
		ClearTrials();
		SetControl(false);
	}

	public void Stop()
	{
		SetControl(true);
		if(!rb.isKinematic) rb.velocity = Vector3.zero;
		if(!rb.isKinematic) rb.angularVelocity = Vector3.zero;
	}

	protected void ClearTrials()
	{
		foreach (var VARIABLE in trails)
		{
			VARIABLE.Clear();
		}
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
	
	public void Teleport(Vector3 point)
	{
		if (carController) carController.enabled = false;

		Vector3 velocity = RB.velocity;
		Vector3 pos = point;
		pos.y = RB.transform.position.y;
		
		RB.velocity = Vector3.zero;
		RB.angularVelocity = Vector3.zero;

		foreach (var VARIABLE in trails)
		{
			VARIABLE.Clear();
			VARIABLE.enabled = false;
		}
		
		RB.position = pos;
		RB.velocity = velocity;
		
		foreach (var VARIABLE in trails)
		{
			VARIABLE.enabled = true;
		}
		
		if (carController) carController.enabled = true;
	}
}