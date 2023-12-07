using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CarButton : MonoBehaviour
{
    public static Action OnUpdate { get; set; }

    public int Index = 0;

    [Space] 
    [SerializeField] private Transform objectViewParent;
    [SerializeField] private TextMeshProUGUI hpText, spdText;

    [Space] 
    [SerializeField] private Transform costGrid;
    [SerializeField] private TextMeshProUGUI costTextPrefab;
    
    [Space] 
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject equippedButton;

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
        SoftCurrency.OnUpdate -= Refresh;
        HardCurrency.OnUpdate -= Refresh;
        PuzzlePeaces.OnUpdate -= Refresh;
        OnUpdate -= Refresh;
    }

    private void Awake()
    {
        _collection = Resources.Load("PRIZES/COLLECTIONS/CarCollection") as PlayerCarCollection;
        
        SoftCurrency.OnUpdate += Refresh;
        HardCurrency.OnUpdate += Refresh;
        PuzzlePeaces.OnUpdate += Refresh;
        OnUpdate += Refresh;
        
        _car = _collection.GetCar(Index);
        Costs = _car.Costs;
        
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
            Buyed = true;
            PlayerIndex.SetCar(Index);

            foreach (var currency in Costs)
            {
                TypedCurrency.Minus(currency.CostType, currency.Cost);
            }
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
