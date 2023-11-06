using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopBasic : CarController
{
    [Space]
    public CopType Type = CopType.Nothing;
    private ScoreTarget _scoreTarget;
    private CopArrestController _arrest;
    
    public void FullHeal() => health.FullHeal();
    public void Heal(int hp) => health.Heal(hp);

    public void GetHit(int dmg)
    {
        if (!_arrest.RequireToArrest)
        {
            health.GetHit(dmg);
        }
    }
    
    public void AddPoint() => _scoreTarget.AddPoint();
    
    public virtual void On(Transform target, Vector3 spawn) { }
    public virtual void Off() { }

    private Transform _target;
    public void SetTarget(Transform trg) => _target = trg;
    public Transform Target => _target;

    protected Vector3 Move
    {
        get
        {
            if (Target)
            {
                Vector3 dir = (Target.position - transform.position).normalized;
                dir.y = 0f;
                return dir;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }

    void Start()
    {
        _arrest = GetComponent<CopArrestController>();
        _scoreTarget = GetComponent<ScoreTarget>();
    }
    
    protected override void FixedUpdate() { }
}
