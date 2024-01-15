using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseButtons : MonoBehaviour
{
    [SerializeField] private Image carButton, skinButton;
    [SerializeField] private GameObject carMenu, skinMenu;
    
    [Space]
    [SerializeField] private Sprite chooseSpr;
    [SerializeField] private Sprite carSpr;
    [SerializeField] private Sprite skinSpr;

    void OnEnable()
    {
        if (skinMenu.activeSelf)
        {
            OpenSkins();
        }
        else
        {
            OpenCars();
        }
    }
    
    public void OpenCars()
    {
        carMenu.SetActive(true);
        skinMenu.SetActive(false);

        skinButton.sprite = skinSpr;
        carButton.sprite = chooseSpr;
    }
    
    public void OpenSkins()
    {
        carMenu.SetActive(false);
        skinMenu.SetActive(true);

        skinButton.sprite = chooseSpr;
        carButton.sprite = carSpr;
    }
}
