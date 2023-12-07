using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Fortune/Create wheel setup")]
public class FortuneWheelSetup : ScriptableObject
{
    [field: SerializeField] public Lot[] Lots;

    [Space] 
    [Range(0f, 1f)]
    [SerializeField] private float saturation;
    [Range(0f, 1f)]
    [SerializeField] private float brightness;

    public Color GetColor(int index)
    {
        // int i = index % 2 != 0 ? index : Lots.Length - index;
        return Color.HSVToRGB(index * 1f / Lots.Length, saturation, brightness);
    }
}

[Serializable]
public struct Lot
{
    [Range(0f, 100f)]
    public float Rarity;
    public Prize Prize;
    public int Count;
}
