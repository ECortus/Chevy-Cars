using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOperator : Instancer<PauseOperator>
{
    protected override void SetInstance()
    {
        Instance = this;
    }

    [SerializeField] private GameObject menu;

    public void On()
    {
        menu.SetActive(true);
        GameManager.Instance.SetTimeScale(0f);
    }
    
    public void Off()
    {
        menu.SetActive(false);
        GameManager.Instance.SetTimeScale(1f);
    }

    public void Resume()
    {
        On();
    }

    public void Restart()
    {
        menu.SetActive(true);
        GameManager.Instance.SetTimeScale(0f);
    }

    public void SetVibration()
    {
        
    }
    
    public void SetSound()
    {
        
    }
    
    public void ExitToLobby()
    {
        GameManager.Instance.SetTimeScale(1f);
        GameManager.Instance.ToLobbyLoader.LoadLobby();
    }
}
