using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DestrictionPool : MonoBehaviour
{
    [Inject] public static DestrictionPool Instance { get; set; }
    [Inject] void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }
    
    [SerializeField] private float delayToSpawn = 30f;

    private bool _isOn = false;
    public bool IsOn => _isOn;

    public void On() => _isOn = true;
    public void Off() => _isOn = false;
    
    public class DestrictionWaiter
    {
        public DestrictionWaiter(DestrictionObject obj, float delay)
        {
            Object = obj;
            Time = delay;
        }
    
        public DestrictionObject Object;
        public float Time;

        public void ResetTime(float delay)
        {
            Time = delay;
        }
    }

    private List<DestrictionWaiter> Waiters = new List<DestrictionWaiter>();

    public void Add(DestrictionObject obj)
    {
        for (int i = 0; i < Waiters.Count; i++)
        {
            if (Waiters[i].Object == obj)
            {
                Waiters[i].ResetTime(delayToSpawn);
                return;
            }
        }

        DestrictionWaiter waiter = new DestrictionWaiter(obj, delayToSpawn);
        Waiters.Add(waiter);
    }

    public void ResetDestroyed(DestrictionObject obj)
    {
        for (int i = 0; i < Waiters.Count; i++)
        {
            if (Waiters[i].Object == obj)
            {
                Waiters[i].ResetTime(delayToSpawn);
                return;
            }
        }
        
        Add(obj);
    }

    public void SpawnAll()
    {
        for (int i = 0; i < Waiters.Count; i++)
        {
            if (Waiters[i] != null)
            {
                Spawn(Waiters[i]);
            }
        }
    }

    public void Spawn(DestrictionWaiter obj)
    {
        obj.Object.Revive();
    }

    public void Remove(DestrictionObject obj)
    {
        for (int i = 0; i < Waiters.Count; i++)
        {
            if (Waiters[i].Object == obj)
            {
                Waiters.Remove(Waiters[i]);
                return;
            }
        }
    }

    void Update()
    {
        if (IsOn)
        {
            for (int i = 0; i < Waiters.Count; i++)
            {
                if (Waiters[i].Object.IsDestroyed)
                {
                    Waiters[i].Time -= Time.deltaTime;
                    if (Waiters[i].Time <= 0f)
                    {
                        Spawn(Waiters[i]);
                        i--;
                    }
                }
            }
        }
    }
}
