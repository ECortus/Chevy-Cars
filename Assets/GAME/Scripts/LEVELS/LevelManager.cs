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
    
    public void SetIndex(int value) => index = Mathf.Clamp(value, 0, Levels.Length - 1);
    public void IncreaseIndex() => SetIndex(Index + 1);
    public void DecreaseIndex() => SetIndex(Index - 1);

    public Level ActualLevel => Levels[Index];

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
        ActualLevel.WinLevel();
    }
    
    public void Lose()
    {
        ActualLevel.LoseLevel();
    }
}
