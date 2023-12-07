using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public static Action OnUpdate { get; set; }

    public int Index = 0;

    [SerializeField] private Transform objectViewParent;
    [SerializeField] private TextMeshProUGUI hpText, spdText;

    [Space] 
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image costImage;
    
    [Space] 
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject equippedButton;

    private PlayerSkinCollection _collection;
    private PlayerSkinCollection.Skin _skin;
    private TypedCurrency.Currency _currency;
    
    private int Cost => _skin.Cost;

    private bool Buyed
    {
        get => PlayerPrefs.GetInt("PlayerSkinBuyed_" + Index, 0) != 0;
        set
        {
            PlayerPrefs.SetInt("PlayerSkinBuyed_" + Index, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    
    private bool Equipped => PlayerIndex.Skin == Index;
    
    private void OnDestroy()
    {
        SoftCurrency.OnUpdate -= Refresh;
        HardCurrency.OnUpdate -= Refresh;
        PuzzlePeaces.OnUpdate -= Refresh;
        OnUpdate -= Refresh;
    }
    
    private void Awake()
    {
        _collection = Resources.Load("PRIZES/COLLECTIONS/SkinCollection") as PlayerSkinCollection;
        
        SoftCurrency.OnUpdate += Refresh;
        HardCurrency.OnUpdate += Refresh;
        PuzzlePeaces.OnUpdate += Refresh;
        OnUpdate += Refresh;
        
        _skin = _collection.GetSkin(Index);
        _currency = _skin.CostType;
        
        Transform view = Instantiate(_skin.Prefab).GetComponent<Transform>();

        if(objectViewParent.childCount > 0) Destroy(objectViewParent.GetChild(0).gameObject);
        view.SetParent(objectViewParent);
        view.transform.localPosition = Vector3.zero;
        view.transform.localEulerAngles = Vector3.zero;
        view.transform.localScale = Vector3.one;
        
        hpText.text = $"HP: +{_skin.HPBonus.ToString()}";
        spdText.text = $"SPD: +{((int)(_skin.SPDBonus * 100f)).ToString()}%";

        costImage.sprite = TypedCurrency.Sprite(_currency);
        costText.text = Cost.ToString();

        if (Index == 0)
        {
            Buyed = true;
        }
        
        OnUpdate?.Invoke();
    }

    public void OnBuyButtonClick()
    {
        if (TypedCurrency.Value(_currency) >= Cost)
        {
            Buyed = true;
            PlayerIndex.SetSkin(Index);
            
            TypedCurrency.Minus(_currency, Cost);
        }
        
        OnUpdate?.Invoke();
    }
    
    public void OnEquipButtonClick()
    {
        PlayerIndex.SetSkin(Index);
        OnUpdate?.Invoke();
    }

    private void Refresh()
    {
        if (!Buyed)
        {
            ChangeButtons(0);
        }
        else if (!Equipped)
        {
            ChangeButtons(1);
        }
        else if (Equipped)
        {
            ChangeButtons(2);
        }
    }

    void ChangeButtons(int index)
    {
        buyButton.SetActive(index == 0);
        equipButton.SetActive(index == 1);
        equippedButton.SetActive(index == 2);
    }
}
