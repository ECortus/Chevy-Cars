using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : BarUI
{
    public HealthData Data { get; set; }
    
    protected override float Amount => Data.HP;
    protected override float MaxAmount => Data.MaxHP;
}
