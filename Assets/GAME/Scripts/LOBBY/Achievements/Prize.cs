using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Prizes/Create prize")]
public class Prize : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    
    [SerializeField] private UnityEvent<int> Event;
    [field: SerializeField] public int Number { get; set; }

    [Space] 
    public ThrowingSpriteAnimationType AnimationType;
    
    public void Get(int number = -1)
    {
        if (number > 0) Number = number;
        Event?.Invoke(Number);

        ThrowingSpritesController.Play(AnimationType);
    }
}
