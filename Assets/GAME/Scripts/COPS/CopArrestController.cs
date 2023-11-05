using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopArrestController : MonoBehaviour
{
    private PlayerController Player => PlayerController.Instance;
    
    private CopController controller;
    [SerializeField] private float distance = 2f, speed = 1f, arrestTime = 3f;

    private float time = 0f;

    void Start()
    {
        controller = GetComponent<CopController>();
        SetTime();
    }

    void Update()
    {
        //Debug.Log(gameObject.name + " on destroy - distance = " + (Player.Transform.position - controller.transform.position).magnitude + ", speed = " + Player.Speed + ", condition: " + distance + ", " + speed + ", so is - " + RequireToArrest);
        if (RequireToArrest && !controller.Died)
        {
            time -= Time.deltaTime;
            if (time <= 0f)
            {
                Arrest();
            }
        }
    }

    void Arrest()
    {
        Player.GetArrested();
        SetTime();
    }

    void SetTime()
    {
        time = arrestTime;
    }

    public bool RequireToArrest => RequireSpeed && RequireDistance;
    bool RequireDistance => (Player.Transform.position - controller.transform.position).magnitude <= distance;
    bool RequireSpeed => Player.Speed <= speed;
}
