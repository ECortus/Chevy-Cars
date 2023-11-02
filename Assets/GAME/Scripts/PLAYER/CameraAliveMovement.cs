using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAliveMovement : MonoBehaviour
{
	[SerializeField] private Transform moveTransform;
	[SerializeField] private float sensitivity = 0.1f, speed = 5f, delay = 2f;
	
	Vector2 originalPos;
    Vector3 target;

    /*void Start()
    {
        On();
    }*/

    public void On()
    {
        originalPos = moveTransform.localPosition;

        StopAllCoroutines();
        StartCoroutine(Shaking());
    }

    public void Off()
    {
        StopAllCoroutines();
        
        target = Vector3.zero;
        moveTransform.localPosition = Vector3.zero;
    }

    void Update()
    {
        moveTransform.localPosition = Vector3.Lerp(moveTransform.localPosition, target, speed * Time.deltaTime);
    }

    IEnumerator Shaking()
    {
        Vector3 random;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while(true)
        {
            random = Random.insideUnitSphere;
            random.y = Mathf.Abs(random.y);
            random.z = 0f;
            
            target = new Vector3(originalPos.x, originalPos.y, moveTransform.localPosition.z) 
                + random * sensitivity;

            yield return wait;
        }
    }
}
