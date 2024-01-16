using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Prizes/Create skin collection")]
public class PlayerSkinCollection : ScriptableObject
{
    [Serializable]
    public struct Skin
    {
        public bool IsForAchievement => Rarity == SkinRarity.Achieved;
        
        [Space]
        public GameObject Prefab;
        public SkinRarity Rarity;
        
        [Space]
        [Range(0, 10)]
        public int HPBonus;
        [Range(0f, 10f)]
        public float SPDBonus;

        [Space] 
        public TypedCurrency.Currency CostType;
        public int Cost;
    }
    
    public Skin[] Skins;
    public Skin GetSkin(int index) => Skins[index % Skins.Length];
}

public enum SkinRarity
{
    Normal, Rare, Achieved
}
