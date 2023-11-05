using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CopsSpawnController : Instancer<CopsSpawnController>
{
    protected override void SetInstance()
    {
        Instance = this;
    }

    [SerializeField] private Transform[] Dots;
    [SerializeField] private float requireDistanceToSpawn = 10f;

    private Transform RequireTarget => PlayerController.Instance.Transform;

    private int Attention => AttentionController.Instance.Attention;
    private CopsSlot CopsSlot => LevelManager.Instance.ActualLevel.CopsRating.GetSlot(Attention);
    private Coroutine _control;
    
    public void On()
    {
        _control ??= StartCoroutine(Control());
    }

    public void Off()
    {
        if (_control != null)
        {
            StopCoroutine(_control);
            _control = null;
        }
    }

    public void Clean()
    {
        CopsPool.Instance.ClearAll();
    }

    private IEnumerator Control()
    {
        CopType type;
        List<CopController> cops;
        
        CopUnit[] units;
        CopUnit unit;
        
        int requireCount = -1;
        int aliveCount = -1;
        Transform spawnDot;
        
        // int attention = -1;
        
        while (true)
        {
            // if (attention != Attention)
            // {
            //     Clean();
            //     attention = Attention;
            // }

            if (Attention > 0)
            {
                units = CopsSlot.Units;

                for (int i = 0; i < units.Length; i++)
                {
                    unit = units[i];

                    for (int j = 0; j < unit.Count; j++)
                    {
                        type = unit.CopPrefab.Type;
                        cops = CopsPool.Instance.GetArray(type);

                        requireCount = unit.Count;
                        aliveCount = 0;

                        for (int k = 0; k < cops.Count; k++)
                        {
                            if (cops[k].IsActive && !cops[k].Died)
                            {
                                aliveCount++;
                            }
                        }

                        if (aliveCount < requireCount)
                        {
                            for (int k = 0; k < requireCount - aliveCount; k++)
                            {
                                spawnDot = RandomDot();
                                CopsPool.Instance.Insert(type, unit.CopPrefab, spawnDot.position);
                            }
                        }
                    }
                }
            }
            
            yield return null;
        }
    }

    Transform RandomDot()
    {
        Transform dot = Dots[Random.Range(0, Dots.Length)];

        if ((dot.position - RequireTarget.position).magnitude <= requireDistanceToSpawn)
        {
            return RandomDot();
        }

        return dot;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        foreach (var VARIABLE in Dots)
        {
            Gizmos.DrawWireSphere(VARIABLE.transform.position, requireDistanceToSpawn);
        }
    }
}
