using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRotating : MonoBehaviour
{
    [SerializeField] private float speed, maxRotateAngle;
    [SerializeField] private Transform toRotate;

    float angle;
    int sign = 1;

    private Vector3 offset;

    void OnEnable()
    {
        offset = toRotate.eulerAngles;
    }

    void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            angle += speed * sign * Time.deltaTime;
            angle = Mathf.Clamp(angle, -maxRotateAngle, maxRotateAngle);

            if(angle == maxRotateAngle)
            {
                sign = -1;
            }
            else if(angle == -maxRotateAngle)
            {
                sign = 1;
            }

            toRotate.eulerAngles = Vector3.Lerp(toRotate.eulerAngles, new Vector3(0f, angle, 0f) + offset, speed * Time.deltaTime);
        }
    }
}