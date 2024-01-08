using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleHandler : MonoBehaviour
{

    Vector2 touch1InitialPosition;
    Vector2 touch2InitialPosition;
    Vector3 InitialScale;
    [SerializeField]
    [Tooltip("Use lower scalling factor for increase the scalling speed")]
    int scaleFactor = 1000;

    [SerializeField]
    [Tooltip("Minimum scale limit")]
    float MinimumValue;

    [SerializeField]
    [Tooltip("Maximum scale limit")]
    float MaximumValue;

    float initalDistance;
    float currentDistance;
    float previousDistance;
    Vector3 previousScale;

    private void Start()
    {
        previousScale = transform.localScale;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touch1InitialPosition = Input.GetTouch(0).position;
            }
        }

        if (Input.touchCount > 1)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                touch2InitialPosition = Input.GetTouch(1).position;
                previousDistance = Vector2.Distance(touch1InitialPosition, touch2InitialPosition);
                InitialScale = gameObject.transform.localScale;
            }        
            currentDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            // console1.text = "d" + scaleFactor(initalDistance, currentDistance);
            changeScale(getScaleFactor(currentDistance));
            //console.text = "scale" + gameObject.transform.localScale;
        }

        limitScale(MinimumValue, MaximumValue);
    }


    float getScaleFactor( float current)
    {
        float value;
        if(previousDistance == current)
        {
            value = 0;  
        }
        else
        {
            value = (current - previousDistance) / scaleFactor;
            previousDistance = current;
        }
        return value;
    }

    void changeScale(float factor)
    {
        gameObject.transform.localScale = gameObject.transform.localScale + InitialScale * (factor);      
    }

    void limitScale(float min,float max)
    {
        
        if (transform.localScale.x < min || transform.localScale.y < min || transform.localScale.z < min)
        {
            transform.localScale = previousScale;
        }
        else if (transform.localScale.x > max || transform.localScale.y > max || transform.localScale.z > max)
        {
            transform.localScale = previousScale;
        }
        else
        {
            previousScale = transform.localScale;
        }
    }
}
