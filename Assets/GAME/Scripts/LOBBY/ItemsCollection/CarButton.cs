using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CarButton : MonoBehaviour
{
    public static Action OnUpdate { get; set; }

    public int Index { get; private set; }
    
    [Serializable]
    public struct CarRaritySprite
    {
        public CarRarity Rarity;
        public Sprite Sprite;
    }

    [SerializeField] private Image mainBg;
    
    [Space]
    [SerializeField] private Transform objectViewParent;
    [SerializeField] private TextMeshProUGUI hpText, spdText;

    [Space] 
    [SerializeField] private Transform costGrid;
    [SerializeField] private TextMeshProUGUI costTextPrefab;

    [Space] 
    [SerializeField] private GameObject forAchievementButton;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject equippedButton;
    
    [Space] 
    [SerializeField] private CarRaritySprite[] RaritySprites;

    private Sprite GetMainSprite(CarRarity rarity)
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

    private PlayerCarCollection _collection;
    private PlayerCarCollection.Car _car;

    private bool Buyed
    {
        get => PlayerPrefs.GetInt("PlayerCarBuyed_" + Index, 0) != 0;
        set
        {
            PlayerPrefs.SetInt("PlayerCarBuyed_" + Index, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    
    private bool Equipped => PlayerIndex.Car == Index;
    
    private PlayerCarCollection.PuzzlesCost[] Costs;

    private bool EnoughResource
    {
        get
        {
            bool value = true;

            foreach (var VARIABLE in Costs)
            {
                if (TypedCurrency.Value(VARIABLE.CostType) < VARIABLE.Cost)
                {
                    value = false;
                    break;
                }
            }
            
            return value;
        }
    }

    private void OnDestroy()
    {
        // Debug.Log("car button destroy " + Index);
        
        SoftCurrency.OnUpdate -= Refresh;
        HardCurrency.OnUpdate -= Refresh;
        PuzzlePeaces.OnUpdate -= Refresh;
        OnUpdate -= Refresh;
    }

    // void Awake()
    // {
    //     Init();
    // }

    public void Init(PlayerCarCollection collection, int index)
    {
        // _collection = Resources.Load("PRIZES/COLLECTIONS/CarCollection") as PlayerCarCollection;
        _collection = collection;
        Index = index;
        
        SoftCurrency.OnUpdate += Refresh;
        HardCurrency.OnUpdate += Refresh;
        PuzzlePeaces.OnUpdate += Refresh;
        OnUpdate += Refresh;
        
        _car = _collection.GetCar(Index);
        Costs = _car.Costs;
        mainBg.sprite = GetMainSprite(_car.Rarity);
        
        Transform view = Instantiate(_car.PrefabObject).GetComponent<Transform>();
    
        if(objectViewParent.childCount > 0) Destroy(objectViewParent.GetChild(0).gameObject);
        view.SetParent(objectViewParent);
        view.transform.localPosition = Vector3.zero;
        view.transform.localEulerAngles = Vector3.zero;
        view.transform.localScale = Vector3.one;
        
        hpText.text = $"HP: {_car.HPBonus.ToString()}";
        spdText.text = $"SPD: +{((int)(_car.SPDBonus * 100f)).ToString()}%";

        foreach (Transform VARIABLE in costGrid)
        {
            Destroy(VARIABLE.gameObject);
        }

        TextMeshProUGUI costText;
        foreach (var VARIABLE in Costs)
        {
            if(VARIABLE.Cost <= 0) continue;
            
            costText = Instantiate(costTextPrefab, costGrid);
            costText.gameObject.SetActive(true);
            costText.GetComponentInChildren<Image>().sprite = TypedCurrency.Sprite(VARIABLE.CostType);
            costText.text = VARIABLE.Cost.ToString();
        }
    
        if (Index == 0)
        {
            Buyed = true;
        }
        
        OnUpdate?.Invoke();
    }
    
    public void OnBuyButtonClick()
    {
        if (EnoughResource)
        {
            foreach (var currency in Costs)
            {
                TypedCurrency.Minus(currency.CostType, currency.Cost);
            }
            
            Unlock();
        }
        
        OnUpdate?.Invoke();
    }
    
    public void OnEquipButtonClick()
    {
        PlayerIndex.SetCar(Index);
        OnUpdate?.Invoke();
    }
    
    private void Refresh()
    {
        if (_car.IsForAchievement && !Buyed)
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
        OnEquipButtonClick();
    }
    
    void ChangeButtons(int index)
    {
        forAchievementButton.SetActive(index == -1);
        buyButton.SetActive(index == 0);
        equipButton.SetActive(index == 1);
        equippedButton.SetActive(index == 2);
    }
}
