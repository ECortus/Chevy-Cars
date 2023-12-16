using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamlessConfines : MonoBehaviour
{
    private Transform PlayerTransform => PlayerController.Instance.Transform;
    private Rigidbody PlayerBody => PlayerController.Instance.RB;
    
    [SerializeField] private float insideRadius = 15f;
    [SerializeField] private float outsideRadius = 25f;
    
    [Space]
    [SerializeField] private float teleportRadius = 25f;

    [Space] 
    [SerializeField] private float absValue = 1f;

    private float PlayerDistanceToCenter => (new Vector3(PlayerTransform.position.x, 0, PlayerTransform.position.z)
        - new Vector3(transform.position.x, 0, transform.position.z)).magnitude;

    void FixedUpdate()
    {
        if (PlayerDistanceToCenter > outsideRadius + absValue || PlayerDistanceToCenter > outsideRadius - absValue)
        {
            Vector3 direction = -PlayerTransform.forward; direction.y = 0;
            Vector3 point = transform.position + direction * insideRadius;
            
            TeleportPlayer(point);
            TeleportNearestEnemies(point);
        }
    }

    void TeleportPlayer(Vector3 point)
    {
        PlayerController.Instance.Teleport(point);
    }

    void TeleportNearestEnemies(Vector3 point)
    {
        List<CopBasic> cops = CopsPool.Instance.GetAllArray();

        CopBasic cop;
        float distance;
        Vector3 diff;

        Vector3 copPos, playerPos;

        for (int i = 0; i < cops.Count; i++)
        {
            cop = cops[i];

            copPos = cop.transform.position; copPos.y = 0;
            playerPos = PlayerTransform.position; playerPos.y = 0;
            
            distance = (copPos - playerPos).magnitude;

            if (distance <= teleportRadius + absValue || distance <= teleportRadius - absValue)
            {
                diff = (copPos - playerPos);
                cop.Teleport(point + diff);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, insideRadius);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, outsideRadius);

        if (PlayerController.Instance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(PlayerTransform.position, teleportRadius);
        }
    }
}
