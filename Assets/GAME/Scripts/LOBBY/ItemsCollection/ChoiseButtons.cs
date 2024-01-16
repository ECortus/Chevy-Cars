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

    void Start()
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
        // carMenu.SetActive(true);
        // skinMenu.SetActive(false);

        carMenu.transform.localPosition = new Vector2(0, 0);
        skinMenu.transform.localPosition = new Vector2(0, -10000);

        skinButton.sprite = skinSpr;
        carButton.sprite = chooseSpr;
    }
    
    public void OpenSkins()
    {
        // carMenu.SetActive(false);
        // skinMenu.SetActive(true);
        
        skinMenu.transform.localPosition = new Vector2(0, 0);
        carMenu.transform.localPosition = new Vector2(0, -10000);

        skinButton.sprite = chooseSpr;
        carButton.sprite = carSpr;
    }
}
