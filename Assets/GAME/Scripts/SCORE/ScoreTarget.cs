using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTarget : MonoBehaviour
{
    [SerializeField] private uint toAdd = 5;

    public void AddPoint()
    {
        Score.Plus(toAdd);
    }
}
