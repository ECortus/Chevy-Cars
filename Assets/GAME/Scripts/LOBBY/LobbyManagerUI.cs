using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject main, shop, wheel, progress, settings;

    void Start()
    {
        OpenMain();
    }
    
    public void CloseAll()
    {
        main.SetActive(false);
        shop.SetActive(false);
        wheel.SetActive(false);
        progress.SetActive(false);
        settings.SetActive(false);
    }
    
    public void OpenMain()
    {
        main.SetActive(true);
        shop.SetActive(false);
        wheel.SetActive(false);
        progress.SetActive(false);
        settings.SetActive(false);
    }
    
    public void OpenShop()
    {
        main.SetActive(false);
        shop.SetActive(true);
    }
    
    public void OpenWheel()
    {
        main.SetActive(false);
        wheel.SetActive(true);
    }
    
    public void OpenProgress()
    {
        main.SetActive(false);
        progress.SetActive(true);
    }
    
    public void OpenSettings()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }
}
