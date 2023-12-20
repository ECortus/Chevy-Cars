using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPlayerAssets : MonoBehaviour
{
    [SerializeField] private PlayerCarCollection _carCollection;
    [SerializeField] private PlayerSkinCollection _skinCollection;

    [Space] 
    [SerializeField] private Transform carView;
    [SerializeField] private Transform skinView;
    
    void Awake()
    {
        PlayerIndex.OnSkinUpdate += RefreshSkin;
        PlayerIndex.OnCarUpdate += RefreshCar;
        
        Refresh();
    }

    private void OnDestroy()
    {
        PlayerIndex.OnSkinUpdate -= RefreshSkin;
        PlayerIndex.OnCarUpdate -= RefreshCar;
    }

    private void Refresh()
    {
        RefreshCar();
        RefreshSkin();
    }
    
    private void RefreshCar()
    {
        PlayerCarCollection.Car car = _carCollection.GetCar(PlayerIndex.Car);
        
        if (carView.childCount > 0)
        {
            foreach (Transform VARIABLE in carView)
            {
                Destroy(VARIABLE.gameObject);
            }
        }
        
        Transform view = Instantiate(car.PrefabObject).GetComponent<Transform>();
        view.SetParent(carView);
        view.transform.localPosition = Vector3.zero;
        view.transform.localEulerAngles = Vector3.zero;
        view.transform.localScale = Vector3.one;
    }
    
    private void RefreshSkin()
    {
        PlayerSkinCollection.Skin skin = _skinCollection.GetSkin(PlayerIndex.Skin);
        
        if (skinView.childCount > 0)
        {
            foreach (Transform VARIABLE in skinView)
            {
                Destroy(VARIABLE.gameObject);
            }
        }
        
        Transform view = Instantiate(skin.Prefab).GetComponent<Transform>();
        view.SetParent(skinView);
        view.transform.localPosition = Vector3.zero;
        view.transform.localEulerAngles = Vector3.zero;
        view.transform.localScale = Vector3.one;
        
        view.transform.GetComponent<Animator>().SetBool("OnScene", true);
    }
}
