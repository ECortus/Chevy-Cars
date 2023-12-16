using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsLoader : MonoBehaviour
{
    [SerializeField] private SkinButton prefab;
    [SerializeField] private Transform parent;

    void Awake()
    {
        PlayerSkinCollection collection = Resources.Load<PlayerSkinCollection>("PRIZES/COLLECTIONS/SkinCollection");
        
        foreach (Transform VARIABLE in parent)
        {
            Destroy(VARIABLE.gameObject);
        }
        
        PlayerSkinCollection.Skin[] Skins = collection.Skins;
        SkinButton button;
        
        int count = Skins.Length;
        for (int i = 0; i < count; i++)
        {
            button = Instantiate(prefab, parent);
            button.Init(collection, i);
        }
    }
}
