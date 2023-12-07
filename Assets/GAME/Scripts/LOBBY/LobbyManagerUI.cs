using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject wheel, progress;

    [Space] [SerializeField] private SlideMenuUI slide;
    
    public static bool OnMain { get; private set; }

    private void Awake()
    {
        OnMain = true;
        transform.localPosition = Vector3.zero;
    }

    public void OpenMain()
    {
        OnMain = true;
        slide.SetPosToMove(0);
    }
    
    public void OpenShop()
    {
        slide.SetPosToMove(1080);
    }
    
    public void SetWheel(bool state)
    {
        OnMain = !state;
        wheel.SetActive(state);
    }
    
    public void SetProgress(bool state)
    {
        OnMain = !state;
        progress.SetActive(state);
    }

    public void OpenCollection()
    {
        slide.SetPosToMove(-1080);
    }
}
