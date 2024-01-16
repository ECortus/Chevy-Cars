using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Prizes/Create collections actions")]
public class CollectionsActions : ScriptableObject
{
    public void UnlockSkin(int index)
    {
        SkinsLoader.Instance.Unlock(index);
    }

    public void UnlockCar(int index)
    {
        CarsLoader.Instance.Unlock(index);
    }
}
