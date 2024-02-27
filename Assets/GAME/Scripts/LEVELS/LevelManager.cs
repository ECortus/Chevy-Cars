using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using UnityEngine;

public class LevelManager : Instancer<LevelManager>
{
    protected override void SetInstance()
    {
        Instance = this;
    }

    [SerializeField] private Level[] Levels;

    [Space] 
    public PlayerCarSpawner PlayerSpawner;
    
    [Space]
    [SerializeField] private GameObject regularUI;
    [SerializeField] private GameObject endlessUI;

    [Space] 
    [SerializeField] private int vibrationMillisecondsTime = 100;
    public static int VibrationMillisecondsTimeOnStartOnEnd => Instance.vibrationMillisecondsTime;

    [Space] 
    [SerializeField] private AchievementObject achievementObject;
    
    private int _index { get { return Statistics.LevelIndex; } set { Statistics.LevelIndex = value; } }
    public int Index => _index % Levels.Length;
    
    public void SetIndex(int value) => _index = Mathf.Clamp(value, 0, 9999);

    public void IncreaseIndex()
    {
        int ind = _index;
        ind++;
        SetIndex(ind);

        if (achievementObject)
        {
            achievementObject.AddCompletedCount();
        }
    }

    public void DecreaseIndex()
    {
        int ind = _index;
        ind--;
        SetIndex(ind);
    }

    public Level ActualLevel => Levels[Index];
    public static bool Ended { get; set; }

    void Start()
    {
        Starting();
    }
    
    public void Starting()
    {
        OffAllLevel();
        ActualLevel.On();

        if (Index % 2 == 0)
        {
            AudioManager.Play(1);
        }
        else
        {
            AudioManager.Play(2);
        }
        
        ActualLevel.StartingLevel();
    }

    public void Restart()
    {
        ActualLevel.RestartLevel();
    }

    public async void Win()
    {
        AppsFlyerEventsSuite.AF_LEVEL_ACHIEVED($"level-{Index.ToString()}", $"score-{Score.Value.ToString()}");
        
        await ActualLevel.WinLevel();
        IncreaseIndex();
    }
    
    public void Lose()
    {
        ActualLevel.LoseLevel();
    }

    void OffAllLevel()
    {
        foreach (var VARIABLE in Levels)
        {
            VARIABLE.Off();
        }
    }
    
    public void SetRegularUI()
    {
        regularUI.SetActive(true);
        endlessUI.SetActive(false);
    }
    
    public void SetEndlessUI()
    {
        regularUI.SetActive(false);
        endlessUI.SetActive(true);
    }
}
