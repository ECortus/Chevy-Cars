using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool IsOver;

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsOver = false;
    }

    // public static bool IsOver()
    // {
    //     if (!Instance.enabled) return false;
    //     
    //     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //     List<RaycastResult> results = new List<RaycastResult>();
    //     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //     
    //     return results.Count > 0;
    // }
}
