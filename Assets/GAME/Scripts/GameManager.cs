using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[ExecuteInEditMode]
public class GameManager : Instancer<GameManager>
{
    protected override void SetInstance() => Instance = this;

    private bool _GameActive = false;
    public void SetActive(bool value) => _GameActive = value;
    public bool isActive => _GameActive;

    public CameraController MyCamera;
    public Joystick Joystick;

    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }
}
