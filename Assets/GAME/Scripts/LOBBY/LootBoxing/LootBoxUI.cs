using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxUI : MonoBehaviour
{
    [SerializeField] private LootBox lootInfo;
    [SerializeField] private GetLootUI getLoot;

    [Space] 
    [SerializeField] private GameObject buyObj;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI costText;
    
    [Space]
    [SerializeField] private GameObject getFreeObj;

    private int Cost => lootInfo.Cost;
    private bool haveFree;

    private void Awake()
    {
        SoftCurrency.OnUpdate += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        SoftCurrency.OnUpdate -= Refresh;
    }

    public void OnButtonGetFreeClick()
    {
        getLoot.Show(lootInfo.GetPrizes());
    }
    
    public void OnButtonBuyClick()
    {
        if (SoftCurrency.Value >= Cost)
        {
            SoftCurrency.Minus(Cost);
            getLoot.Show(lootInfo.GetPrizes());
        }
    }
    
    void Refresh()
    {
        if (haveFree)
        {
            ChangeObject(1);
            
            buyButton.interactable = false;
        }
        else if (SoftCurrency.Value < Cost)
        {
            ChangeObject(0);
            buyButton.interactable = false;
            
            costText.text = $"{Cost.ToString()}";
        }
        else
        {
            ChangeObject(0);
            costText.text = $"{Cost.ToString()}";
            
            buyButton.interactable = true;
        }
    }

    void ChangeObject(int index)
    {
        buyObj.SetActive(index == 0);
        getFreeObj.SetActive(index == 1);
    }
}
