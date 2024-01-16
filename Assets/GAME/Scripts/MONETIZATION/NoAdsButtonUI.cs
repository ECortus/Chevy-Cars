using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAdsButtonUI : MonoBehaviour
{
    private void Awake()
    {
        Refresh();
    }

    void Refresh()
    {
        gameObject.SetActive(!GameAds.NoAds);
    }

    public void OnButtonClick()
    {
        if (true)
        {
            GameAds.SetNoAds(true);
            Refresh();;
        }
    }
}
