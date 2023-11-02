using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionController : Instancer<AttentionController>
{
    protected override void SetInstance()
    {
        Instance = this;
    }

    private int _attention;
    public int Attention => _attention;
    public void SetAttention(int value) => _attention = Mathf.Clamp(value, 0, 999);
}
