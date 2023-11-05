using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Instancer<LevelManager>
{
    protected override void SetInstance()
    {
        Instance = this;
    }

    [SerializeField] private Level[] Levels;

    private int index { get { return Statistics.LevelIndex; } set { Statistics.LevelIndex = value; } }
    public int Index => index % Levels.Length;
    
    public void SetIndex(int value) => index = Mathf.Clamp(value, 0, 9999);

    public void IncreaseIndex()
    {
        int ind = index;
        ind++;
        SetIndex(ind);
    }

    public void DecreaseIndex()
    {
        int ind = index;
        ind--;
        SetIndex(ind);
    }

    public Level ActualLevel => Levels[Index];

    void Start()
    {
        Starting();
    }
    
    public void Starting()
    {
        ActualLevel.StartingLevel();
    }

    public void Restart()
    {
        ActualLevel.RestartLevel();
    }

    public void Win()
    {
        IncreaseIndex();
        ActualLevel.WinLevel();
    }
    
    public void Lose()
    {
        ActualLevel.LoseLevel();
    }
}
