using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public static Action OnUpdate { get; set; }

    public int Index { get; private set; }

    [Serializable]
    public struct SkinRaritySprite
    {
        public SkinRarity Rarity;
        public Sprite Sprite;
    }

    [SerializeField] private Image mainBg;
    
    [Space]
    [SerializeField] private Transform objectViewParent;
    [SerializeField] private TextMeshProUGUI hpText, spdText;

    [Space] 
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image costImage;
    
    [Space] 
    [SerializeField] private GameObject forAchievementButton;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject equippedButton;

    [Space] 
    [SerializeField] private SkinRaritySprite[] RaritySprites;

    private Sprite GetMainSprite(SkinRarity rarity)
    {
        Sprite spr = null;

        foreach (var VARIABLE in RaritySprites)
        {
            if (VARIABLE.Rarity == rarity)
            {
                spr = VARIABLE.Sprite;
                break;
            }
        }
        
        return spr;
    }

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
    
    // void Awake()
    // {
    //     Init();
    // }

    public void Init(PlayerSkinCollection collection, int index)
    {
        // _collection = Resources.Load("PRIZES/COLLECTIONS/SkinCollection") as PlayerSkinCollection;
        _collection = collection;
        Index = index;
        
        SoftCurrency.OnUpdate += Refresh;
        HardCurrency.OnUpdate += Refresh;
        PuzzlePeaces.OnUpdate += Refresh;
        OnUpdate += Refresh;
        
        _skin = _collection.GetSkin(Index);
        _currency = _skin.CostType;
        mainBg.sprite = GetMainSprite(_skin.Rarity);
        
        Transform view = Instantiate(_skin.Prefab).GetComponent<Transform>();

        if(objectViewParent.childCount > 0) Destroy(objectViewParent.GetChild(0).gameObject);
        view.SetParent(objectViewParent);
        view.transform.localPosition = Vector3.zero;
        view.transform.localEulerAngles = Vector3.zero;
        view.transform.localScale = Vector3.one;
        
        Animator animator = view.transform.GetComponentInChildren<Animator>();
        // animator.enabled = false;
        animator.enabled = true;
        animator.SetBool("OnScene", false);
        
        hpText.text = $"HP: +{_skin.HPBonus.ToString()}";
        spdText.text = $"SPD: +{((int)(_skin.SPDBonus * 100f)).ToString()}%";

        costImage.sprite = TypedCurrency.Sprite(_currency);
        costText.text = Cost.ToString();

        if (Index == 0)
        {
            Unlock();
        }
        
        _skin.RelativeButton = this;
        
        OnUpdate?.Invoke();
    }

    public void OnBuyButtonClick()
    {
        if (TypedCurrency.Value(_currency) >= Cost)
        {
            Unlock();
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
        if (_skin.IsForAchievement && !Buyed)
        {
            ChangeButtons(-1);
        }
        else if (!Buyed)
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

    public void Unlock()
    {
        Buyed = true;
        OnUpdate?.Invoke();
    }
    
    public void Lock()
    {
        Buyed = false;
        OnUpdate?.Invoke();
    }

    void ChangeButtons(int index)
    {
        forAchievementButton.SetActive(index == -1);
        buyButton.SetActive(index == 0);
        equipButton.SetActive(index == 1);
        equippedButton.SetActive(index == 2);
    }
}
