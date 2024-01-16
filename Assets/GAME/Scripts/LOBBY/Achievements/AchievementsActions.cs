using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievement/Create action collection")]
public class AchievementsActions : ScriptableObject
{
    public void AddSoft(int amount)
    {
        SoftCurrency.Plus(amount);
    }
    
    public void AddHard(int amount)
    {
        HardCurrency.Plus(amount);
    }
}
