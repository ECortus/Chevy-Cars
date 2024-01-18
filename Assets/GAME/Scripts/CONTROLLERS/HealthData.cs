using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthData : MonoBehaviour
{
    [SerializeField] private int maxHP = 1;
    public int MaxHP => maxHP + _bonus;
    private int _bonus;
    public void SetBonus(int bns) => _bonus = bns;

    [SerializeField] private HealthBarUI bar;

    private int _hp;
    public int HP
    {
        get => _hp;
        private set
        {
            _hp = value;

            if (bar)
            {
                if (!bar.Data) bar.Data = this;
                bar.Refresh();
            }
        }
    }

    [Space] 
    [SerializeField] private bool VehicleHitImmunite = false;
    [SerializeField] private bool AmmoHitImmunite = false;
    
    [Space]
    [SerializeField] protected UnityEvent revive;
    [SerializeField] protected UnityEvent death;

    public void FullHeal()
    {
        Heal(MaxHP);
        Revive();
    }
    public bool Died { get; set; }

    public void Heal(int hp)
    {
        if (HP == 0 && HP + hp > 0)
        {
            Died = false;
            revive?.Invoke();
        }

        HP += hp;
        if (HP > MaxHP) HP = MaxHP;
    }

    public void GetHit(int hp, HitType type)
    {
        if (type == HitType.Vehicle && VehicleHitImmunite
            || type == HitType.Ammo && AmmoHitImmunite) return;
        
        HP -= hp;
        if (HP < 0) HP = 0;
        
        if (HP == 0 && !Died)
        {
            Died = true;
            Death();
        }
    }

    void Revive()
    {
        if (bar) bar.gameObject.SetActive(true);
        revive?.Invoke();
    }

    void Death()
    {
        if (bar) bar.gameObject.SetActive(false);
        death?.Invoke();
    }
}

public enum HitType
{
    Vehicle, Ammo, Permanent
}
