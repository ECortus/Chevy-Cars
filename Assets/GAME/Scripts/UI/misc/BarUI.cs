using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BarUI : MonoBehaviour
{
    protected abstract float Amount { get; }
    protected abstract float MaxAmount { get; }

    [SerializeField] private Slider slider;

    public void Refresh()
    {
        slider.minValue = 0f;
        slider.maxValue = MaxAmount;

        slider.value = Amount;
    } 
}
