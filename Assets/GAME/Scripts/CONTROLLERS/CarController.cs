using System;
using UnityEngine;
using DavidJalbert;

public abstract class CarController : MonoBehaviour
{
	[SerializeField] private TinyCarController carController;
	[SerializeField] private Rigidbody rb;

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
}