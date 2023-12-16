using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private ParticleType effectType;
    [SerializeField] private ParticleSystem hitEffect;
    
    public void Shoot()
    {
        CopBasic boss = GetBoss();
        if (boss)
        {
            boss.GetHit(1, HitType.Ammo);
            ParticlePool.Instance.Insert(effectType, hitEffect, boss.transform.position);
        }
    }

    CopBasic GetBoss()
    {
        List<CopBasic> bosses = CopsPool.Instance.GetArray(CopType.Boss);
        
        CopBasic boss = null;
        float min = Mathf.Infinity;
        
        for (int i = 0; i < bosses.Count; i++)
        {
            if (!boss)
            {
                boss = bosses[i];
                min = (boss.transform.position - PlayerController.Instance.Transform.position).magnitude;
                continue;
            }

            if ((boss.transform.position - PlayerController.Instance.Transform.position).magnitude < min)
            {
                boss = bosses[i];
            }
        }

        return boss;
    }
}
