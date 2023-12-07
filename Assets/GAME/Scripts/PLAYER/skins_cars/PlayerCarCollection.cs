using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Prizes/Create car collection")]
public class PlayerCarCollection : ScriptableObject
{
    [Serializable]
    public struct Car
    {
        public PlayerController Prefab;
        public GameObject PrefabObject => Prefab.Transform.GetChild(0).gameObject;
        [Range(0, 10)]
        public int HPBonus;
        [Range(0f, 10f)]
        public float SPDBonus;

        [Space] 
        public PuzzlesCost[] Costs;
    }
    
    [Serializable]
    public struct PuzzlesCost
    {
        public TypedCurrency.Currency CostType;
        public int Cost;
    }
    
    [field: SerializeField] private Car[] Cars;
    public Car GetCar(int index) => Cars[index % Cars.Length];
}
