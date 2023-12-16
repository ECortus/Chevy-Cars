using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ThrowingSprite : MonoBehaviour
{
    public ThrowingSpriteAnimationType Type = ThrowingSpriteAnimationType.Default;
    public Transform EndPosition;
        
    public bool Playing { get; set; }
}

[Serializable]
public enum ThrowingSpriteAnimationType
{
    Default, Soft, Hard, Car
}
