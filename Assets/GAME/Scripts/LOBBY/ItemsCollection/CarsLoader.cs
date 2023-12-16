using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsLoader : MonoBehaviour
{
    [SerializeField] private CarButton prefab;
    [SerializeField] private Transform parent;

    void Awake()
    {
        PlayerCarCollection collection = Resources.Load<PlayerCarCollection>("PRIZES/COLLECTIONS/CarCollection");
        
        foreach (Transform VARIABLE in parent)
        {
            Destroy(VARIABLE.gameObject);
        }
        
        PlayerCarCollection.Car[] Cars = collection.Cars;
        CarButton button;
        
        int count = Cars.Length;
        for (int i = 0; i < count; i++)
        {
            button = Instantiate(prefab, parent);
            button.Init(collection, i);
        }
    }
}