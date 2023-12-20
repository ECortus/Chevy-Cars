using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class WinOperator : Instancer<WinOperator>
{
    protected override void SetInstance()
    {
        Instance = this;
        alpha = menuBg.color.a;
    }
    
    [SerializeField] private GameObject menu;
    [SerializeField] private Image menuBg;
    [SerializeField] private Transform title, quitButton;
    [SerializeField] private TextMeshProUGUI coinText;

    [Space] 
    [SerializeField] private float coinTime = 2f;

    private float alpha;

    private int CoinCount;
    private Coroutine _coroutine;

    public async void On(int coins)
    {
        Hide();
        
        menu.SetActive(true);
        GameManager.Instance.SetTimeScale(0f);
        
        await menuBg.DOColor(new Color(menuBg.color.r, menuBg.color.g, menuBg.color.b, alpha), 0.5f).SetUpdate(true).AsyncWaitForCompletion();
        await title.DOScale(Vector3.one, 0.25f).SetUpdate(true).AsyncWaitForCompletion();
        
        CoinCount = coins;
        
        if (_coroutine != null) StopCoroutine(_coroutine);
        StartCoroutine(Coins());
        
        await coinText.transform.DOScale(Vector3.one, 0.25f).SetUpdate(true).AsyncWaitForCompletion();
        await quitButton.DOScale(Vector3.one, 0.35f).SetUpdate(true).AsyncWaitForCompletion();
    }

    IEnumerator Coins()
    {
        float time = coinTime;
        int coin = 0;
        
        while (time > 0)
        {
            coin = (int)(CoinCount *  (coinTime - time) / coinTime);
            coinText.text = $"{coin.ToString()}";
            
            time -= Time.unscaledDeltaTime;
            yield return null;
        }

        coin = CoinCount;
        coinText.text = $"{coin.ToString()}";
    }
    
    void Hide()
    {
        menu.SetActive(false);

        menuBg.color = new Color(menuBg.color.r, menuBg.color.g, menuBg.color.b, 0);
        title.localScale = Vector3.zero;
        quitButton.localScale = Vector3.zero;
        coinText.transform.localScale = Vector3.zero;
    }
    
    public void ToLobby()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        Hide();
        
        GameManager.Instance.SetTimeScale(1f);
        LevelManager.Instance.ActualLevel.ResetToDefault();
        
        GameManager.Instance.ToLobbyLoader.LoadLobby();
    }
}
