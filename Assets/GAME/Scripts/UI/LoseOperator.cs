using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoseOperator : Instancer<LoseOperator>
{
    protected override void SetInstance()
    {
        Instance = this;
        alpha = menuBg.color.a;
    }

    [SerializeField] private GameObject menu;
    [SerializeField] private Image menuBg;
    [SerializeField] private Transform title, restartButton, quitButton;

    private float alpha = 0;

    public async void On()
    {
        Hide();
        
        menu.SetActive(true);
        GameManager.Instance.SetTimeScale(0f);
        
        await menuBg.DOColor(new Color(menuBg.color.r, menuBg.color.g, menuBg.color.b, alpha), 0.5f).SetUpdate(true).AsyncWaitForCompletion();
        await title.DOScale(Vector3.one, 0.25f).SetUpdate(true).AsyncWaitForCompletion();
        
        await restartButton.DOScale(Vector3.one, 0.35f).SetUpdate(true).AsyncWaitForCompletion();
        await quitButton.DOScale(Vector3.one, 0.35f).SetUpdate(true).AsyncWaitForCompletion();
    }

    void Hide()
    {
        menu.SetActive(false);

        menuBg.color = new Color(menuBg.color.r, menuBg.color.g, menuBg.color.b, 0);
        title.localScale = Vector3.zero;
        restartButton.localScale = Vector3.zero;
        quitButton.localScale = Vector3.zero;
    }
    
    public void Restart()
    {
        LevelManager.Instance.Restart();
        Hide();
        GameManager.Instance.SetTimeScale(1f);
    }
    
    public void BackToLobby()
    {
        GameManager.Instance.SetTimeScale(1f);
        LevelManager.Instance.ActualLevel.ResetToDefault();
        
        GameManager.Instance.ToLobbyLoader.LoadLobby();
    }
}
