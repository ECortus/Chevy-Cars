using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[ExecuteInEditMode]
public class GameManager : Instancer<GameManager>
{
    protected override void SetInstance() => Instance = this;
    
    public static bool GameStarted { get; set; }

    public Joystick Joystick;
    [Space] 
    public LoadLobbyManager ToLobbyLoader;

    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        GameStarted = false;
    }

    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }
}
