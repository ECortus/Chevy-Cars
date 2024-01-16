using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CarsLoader : MonoBehaviour
{
    public static CarsLoader Instance { get; private set; }
    
    [SerializeField] private CarButton prefab;
    [SerializeField] private Transform parent;

    private List<CarButton> List = new List<CarButton>();

    public void Unlock(int index)
    {
        List[index].Unlock();
    }
    
    void Awake()
    {
        PlayerCarCollection collection = Resources.Load<PlayerCarCollection>("PRIZES/COLLECTIONS/CarCollection");
        
        foreach (Transform VARIABLE in parent)
        {
            Destroy(VARIABLE.gameObject);
        }
        
        PlayerCarCollection.Car[] Cars = collection.Cars;
        CarButton button;
        
        List.Clear();
        
        int count = Cars.Length;
        for (int i = 0; i < count; i++)
        {
            button = Instantiate(prefab, parent);
            button.Init(collection, i);
            
            List.Add(button);
        }

        Instance = this;
    }
}