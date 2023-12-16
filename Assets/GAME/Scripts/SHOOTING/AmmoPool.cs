using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : BasePool<AmmoPool, Ammo, AmmoType>
{
    protected override void SetInstance() => Instance = this;

    protected override void InsertAction(Ammo obj, Vector3 pos, Quaternion rot)
    {
        // obj.rb.position = pos;
        
        // if(rot != new Quaternion()) obj.rb.rotation = rot;
        // obj.On();
    }
    
    protected override bool Condition(Ammo obj)
    {
        return !obj.Active;
    }
}
