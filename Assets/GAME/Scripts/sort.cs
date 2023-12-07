using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sort : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 space;
    
    [ContextMenu("sort")]
    public void form()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).localPosition = new Vector3(space.x * (i % 5), 0, space.z * (i / 5));
        }
    }
}
