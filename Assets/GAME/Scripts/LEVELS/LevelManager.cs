using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Instancer<LevelManager>
{
    protected override void SetInstance()
    {
        Instance = this;
    }
    
    [SerializeField] private List<Level> Levels = new List<Level>();

    private int _Index { get { return Statistics.LevelIndex; } set { Statistics.LevelIndex = value; } }
    public int GetIndex() => _Index % Levels.Count;
    public void SetIndex(int value)
    {
        if(value < 0) _Index = 0;
        else _Index = value;
    }

    public Level ActualLevel => Levels[GetIndex()];
}
