using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : BasePool<ParticlePool, ParticleSystem, ParticleType>
{
    protected override void SetInstance() => Instance = this;

    protected override void InsertAction(ParticleSystem obj, Vector3 pos, Quaternion rot)
    {
        obj.transform.position = pos;
        if(rot != new Quaternion()) obj.transform.rotation = rot;
                
        obj.Play();
    }
    
    protected override bool Condition(ParticleSystem obj)
    {
        return obj.isPlaying;
    }
}
