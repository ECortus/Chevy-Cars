using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOperator : Instancer<WinOperator>
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
    
    public void ToLobby()
    {
        GameManager.Instance.SetTimeScale(1f);
        GameManager.Instance.ToLobbyLoader.LoadLobby();
    }
}
