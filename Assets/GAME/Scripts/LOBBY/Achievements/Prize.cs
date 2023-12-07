using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Prizes/Create prize")]
public class Prize : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    
    [SerializeField] private UnityEvent<int> Event;
    [field: SerializeField] public int Count { get; set; }

    public void Get(int count = -1)
    {
        if (count > 0) Count = count;
        Event?.Invoke(Count);
    }
}
