using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DavidJalbert;

public abstract class CarController : MonoBehaviour
{
	[SerializeField] private TinyCarController carController;
	[SerializeField] private Rigidbody rb;
	
	[Space]
	[SerializeField] private HealthData health;

	public void FullHeal() => health.FullHeal();
	public void Heal(int hp) => health.Heal(hp);
	public void GetHit(int dmg) => health.GetHit(dmg);
	public bool Died => health.Died;

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

	protected abstract void FixedUpdate();

	protected void SetMotor(uint value) => carController.setMotor(value);
	
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
		rb.velocity = Vector3.zero;
	}

	protected void ClearTrials()
	{
		if(rightTrial) rightTrial.Clear();
		if(leftTrial) leftTrial.Clear();
	}
	
	[Space] 
	[SerializeField] private RagdollController ragdoll;
	[SerializeField] private ParticleSystem destroyingEffect;

	public async UniTask SetDefaultRagdoll()
	{
		await ragdoll.SetDefault();
	}
	
	public virtual void Destroying()
	{
		ragdoll.ForceFromDot(transform.position, 250f);
        
		if (destroyingEffect)
		{
			ParticlePool.Instance.Insert(ParticleType.BlowUpCar, destroyingEffect, transform.position + Vector3.up * 1.5f);
		}
	}
}