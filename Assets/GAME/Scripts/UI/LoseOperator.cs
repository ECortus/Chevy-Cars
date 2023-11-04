using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOperator : Instancer<LoseOperator>
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
    
    public void Restart()
    {
        menu.SetActive(false);
        GameManager.Instance.SetTimeScale(1f);
        
        LevelManager.Instance.Restart();
    }
    
    public void BackToLobby()
    {
        GameManager.Instance.SetTimeScale(1f);
        GameManager.Instance.ToLobbyLoader.LoadLobby();
    }
}
