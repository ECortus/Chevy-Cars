using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchIndicatorHandler : MonoBehaviour
{
    string hitObjectName;
    public static bool isTouchedTheObject;
    void Start()
    {
        hitObjectName = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary || Input.touches[0].phase == TouchPhase.Moved))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                hitObjectName = Hit.transform.tag;
                if (hitObjectName == "ARObject")
                {
                    PlaceOnPlane.showTouchIndicator();
                    isTouchedTheObject = true;
                }
                else
                {
                    if (!isTouchedTheObject)
                    {
                        PlaceOnPlane.hideTouchIndicator();
                    }                  
                }
            }

        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isTouchedTheObject = false;
            PlaceOnPlane.hideTouchIndicator();
        }
        else
        {
            if (!isTouchedTheObject)
            {
                PlaceOnPlane.hideTouchIndicator();
            }
        }
    }
}