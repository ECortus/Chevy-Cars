using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootBox : MonoBehaviour
{
    [Serializable]
    public struct PuzzleRarity
    {
        public PuzzlePeaceType Type;
        [Range(0, 100)] 
        public int Rarity;

        [Space] 
        public int minCount;
        public int maxCount;
    }

    [SerializeField] private PuzzlePeaces collection;
    
    [Space]
    [SerializeField] private int prizesCount;
    public int Cost = 25;
    
    [Space]
    [SerializeField] private PuzzleRarity[] Rarities;

    public Prize[] GetPrizes()
    {
        Prize[] list = new Prize[prizesCount];

        int allRarity = 0;
        foreach (var VARIABLE in Rarities)
        {
            allRarity += VARIABLE.Rarity;
        }

        int rarityIndex = 0;

        for (int j = 0; j < prizesCount; j++)
        {
            rarityIndex = Random.Range(0, allRarity + 1);
        
            for (int i = 0; i < Rarities.Length; i++)
            {
                rarityIndex -= Rarities[i].Rarity;
                if (rarityIndex <= 0)
                {
                    list[j] = collection.GetPuzzleByType(Rarities[i].Type).Prize;
                    list[j].Number = Random.Range(Rarities[i].minCount, Rarities[i].maxCount + 1);
                    break;
                }
            }
        }
        
        return list;
    }
}
