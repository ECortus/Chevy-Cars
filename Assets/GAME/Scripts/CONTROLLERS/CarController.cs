using System;
using UnityEngine;
using DavidJalbert;

public abstract class CarController : MonoBehaviour
{
	[SerializeField] private TinyCarController carController;
	[SerializeField] private Rigidbody rb;
	
	[Space]
	[SerializeField] private float rotateSpeed;
	
	public Rigidbody RB => rb;
	public float RotateSpeed => rotateSpeed;

	private bool _takeControl;
	public bool TakeControl => _takeControl;
	public void SetControl(bool value) => _takeControl = value;

	protected abstract void FixedUpdate();

	public void SetMotor(uint value) => carController.setMotor(value);
}