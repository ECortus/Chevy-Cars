using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkinsLoader : MonoBehaviour
{
    public static SkinsLoader Instance { get; private set; }
    
    [SerializeField] private SkinButton prefab;
    [SerializeField] private Transform parent;

    private List<SkinButton> List = new List<SkinButton>();
    public void Unlock(int index) => List[index].Unlock();
    
    void Awake()
    {
        PlayerSkinCollection collection = Resources.Load<PlayerSkinCollection>("PRIZES/COLLECTIONS/SkinCollection");
        
        foreach (Transform VARIABLE in parent)
        {
            Destroy(VARIABLE.gameObject);
        }
        
        PlayerSkinCollection.Skin[] Skins = collection.Skins;
        SkinButton button;
        
        List.Clear();
        
        int count = Skins.Length;
        for (int i = 0; i < count; i++)
        {
            button = Instantiate(prefab, parent);
            button.Init(collection, i);
            
            List.Add(button);
        }
        
        Instance = this;
    }
}
