using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopArrestController : MonoBehaviour
{
    private PlayerController Player => PlayerController.Instance;
    
    private CopBasic controller;
    [SerializeField] private float distance = 2f, speed = 1f, _arrestTime = 3f;

    private static float time = 0f, arrestTime = 0f;

    void Start()
    {
        controller = GetComponent<CopBasic>();
        arrestTime = _arrestTime;
        
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

    static void SetTime()
    {
        time = arrestTime;
    }

    public static void Reset()
    {
        SetTime();
    }
    
    public void _Reset()
    {
        SetTime();
    }

    public bool RequireToArrest => RequireSpeed && RequireDistance;
    bool RequireDistance => (Player.Transform.position - controller.Center).magnitude <= distance;
    bool RequireSpeed => Player.Speed <= speed || Player.GetMotor() <= 0;
}
