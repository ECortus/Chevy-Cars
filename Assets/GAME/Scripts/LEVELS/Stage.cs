using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stage
{
    [SerializeField] private uint _ScoreGoal = 10;
    [SerializeField] private StageCompletedEvent _Event;

    public uint ScoreGoal => _ScoreGoal;
    public void OnEvent() => _Event?.Invoke(this);
}
