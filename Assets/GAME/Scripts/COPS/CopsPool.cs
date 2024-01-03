using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopsPool : BasePool<CopsPool, CopBasic, CopType>
{
    protected override void SetInstance() => Instance = this;

    private Transform MainTarget => PlayerController.Instance.Transform;

    protected override void InsertAction(CopBasic obj, Vector3 pos, Quaternion rot)
    {
        obj.On(MainTarget, pos);
    }
    
    protected override bool Condition(CopBasic obj)
    {
        return !obj.IsActive;
    }
    
    public void ClearAll()
    {
        List<CopBasic> cops;
        CopBasic cop;
        
        for (int i = 0; i < Pools.Length; i++)
        {
            cops = Pools[i].List;
            for (int j = 0; j < cops.Count; j++)
            {
                cop = cops[j];
                cop.Off();
            }
        }
    }

    public void KillAll()
    {
        List<CopBasic> cops;
        CopBasic cop;
        
        for (int i = 0; i < Pools.Length; i++)
        {
            cops = Pools[i].List;
            for (int j = 0; j < cops.Count; j++)
            {
                cop = cops[j];
                cop.GetHit(999, HitType.Permanent);
            }
        }
    }

    public void KillAllOnDistanceFromTarget(Transform target, float distance) =>
        GetHitAllOnDistanceFromTarget(target, distance, 999);
    
    public void GetHitAllOnDistanceFromTarget(Transform target, float distance, int damage)
    {
        List<CopBasic> cops;
        CopBasic cop;
        
        for (int i = 0; i < Pools.Length; i++)
        {
            cops = Pools[i].List;
            for (int j = 0; j < cops.Count; j++)
            {
                cop = cops[j];
                if ((target.position - cop.transform.position).magnitude <= distance && cop.Type != CopType.Boss)
                {
                    cop.GetHit(damage, HitType.Permanent);
                }
            }
        }
    }
}
