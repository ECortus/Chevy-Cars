using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthData : MonoBehaviour
{
    [SerializeField] private int maxHP = 1;
    private int HP { get; set; }

    [SerializeField] private UnityEvent revive, death;

    public void FullHeal() => Heal(maxHP);
    public bool Died { get; set; }

    public void Heal(int hp)
    {
        if (HP == 0 && HP + hp > 0)
        {
            Died = false;
            revive?.Invoke();
        }

        HP += hp;
        if (HP > maxHP) HP = maxHP;
    }

    public void GetHit(int hp)
    {
        HP -= hp;
        if (HP < 0) HP = 0;
        
        if (HP == 0 && !Died)
        {
            Died = true;
            Death();
        }
    }
    
    void Revive() => revive?.Invoke();
    void Death() => death?.Invoke();
}
