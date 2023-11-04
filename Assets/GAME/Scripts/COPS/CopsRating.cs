using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rate", menuName = "Cops Rate")]
[System.Serializable]
public class CopsRating : ScriptableObject
{
    [SerializeField] private CopsSlot[] Slots;
    
    public int MinSearchRate => 1;
    public int MaxSearchRate => Slots.Length;

    public CopsSlot[] GetSlots() => Slots;
    
    public CopsSlot GetSlot(int ind)
    {
        if (ind < MinSearchRate) return null;
        
        int index = Mathf.Clamp(ind, 1, MaxSearchRate) - 1;
        return Slots[index];
    }
}

[System.Serializable]
public class CopsSlot
{
    [Range(0, 100f)]
    public float GoalPercentToReachThisRating = 15;
    public CopUnit[] Units;
}

[System.Serializable]
public class CopUnit
{
    public CopController CopPrefab;
    public int Count;
}
