using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Prizes/Create collections actions")]
public class CollectionsActions : ScriptableObject
{
    [SerializeField] private PlayerSkinCollection _skinCollection;
    [SerializeField] private PlayerCarCollection _carCollection;
    
    public void UnlockSkin(int index)
    {
        _skinCollection.Skins[index].RelativeButton.Unlock();
    }

    public void UnlockCar(int index)
    {
        _carCollection.Cars[index].RelativeButton.Unlock();
    }
}
