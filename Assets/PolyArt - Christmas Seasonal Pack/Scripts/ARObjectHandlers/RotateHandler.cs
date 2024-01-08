using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 touch1InitialPosition;
    Vector2 touch2InitialPosition;
    Quaternion InitialRotation;
    public int scaleFactor = 1000;
    float initalRotation;
    float currentRotation;
    float previousRotation;
  //  public Text console;

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

        if ( Input.touchCount > 1)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                touch2InitialPosition = Input.GetTouch(1).position;
                previousRotation = Vector2.Angle(touch1InitialPosition, touch2InitialPosition);
            }
            currentRotation = Vector2.Angle(Input.GetTouch(0).position, Input.GetTouch(1).position);
            // console1.text = "d" + scaleFactor(initalDistance, currentDistance);
            changeRotation(getRotateFactor(currentRotation));
            
        }
    }


    float getRotateFactor(float current)
    {
        float value;
        if (previousRotation == current)
        {
            value = 0;
        }
        else
        {
            value = (current - previousRotation) * scaleFactor;
            previousRotation = current;
        }    
        //console.text = "r"+ value + gameObject.transform.rotation;
        return value;
    }

    void changeRotation(float factor)
    {

        // transform.rotation =  Quaternion.AngleAxis(factor, Vector3.up);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + factor, 0);
    }
}
