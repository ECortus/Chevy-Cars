using System;
using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using ModestTree;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Achievement00", menuName = "Achievement/Create achievement object")]
public class AchievementObject : ScriptableObject
{
    // public Action OnUpdate { get; set; }
    
    private string Name => name;
    
    [Serializable]
    public struct RewardSlot
    {
        [field: SerializeField] public int ToCompleteCount { get; private set; }
        [Space]
        [SerializeField] public Prize Prize;
        [SerializeField] public int PrizeCount;
        
        public void Load(AchievementObject main, int index)
        {
            Main = main;
            Index = index;
            HaveCompleted = Main.ProgressLevel > Index;
        }

        private AchievementObject Main;
        private int Index;
        private bool HaveCompleted;

        public void Complete()
        {
            if (!HaveCompleted)
            {
                // Debug.Log("complete");
                
                Prize.Get(PrizeCount);
                HaveCompleted = true;

                Main.ProgressLevel = Index;

                AppsFlyerEventsSuite.AF_BONUS_CLAIMED($"achievement-{Main.Name}-stage-{Main.ProgressLevel.ToString()}");
            }
        }
    }

    [SerializeField] private RewardSlot[] Slots;

    public RewardSlot GetCurrentSlot() => Slots[ProgressLevel + 1];
    
    [field: SerializeField] public Sprite Sprite { get; private set; }

    private int MaxLevel => Slots.Length;
    public bool IsMax => ProgressLevel + 1 == MaxLevel;
    
    public int ProgressLevel
    {
        get => PlayerPrefs.GetInt(Name + "_ProgressLVL", -1);
        private set
        {
            PlayerPrefs.SetInt(Name + "_ProgressLVL", Mathf.Clamp(value, 0, MaxLevel));
            PlayerPrefs.Save();
        }
    }
    
    public int CompletedCount
    {
        get => PlayerPrefs.GetInt(Name + "_CompletedCount", 0);
        private set
        {
            PlayerPrefs.SetInt(Name + "_CompletedCount", value);
            PlayerPrefs.Save();
        }
    }
    
    private int ToCompleteGoal
    {
        get
        {
            int count = 0;

            for(int i = ProgressLevel + 1; i > -1; i--)
            {
                count += Slots[i].ToCompleteCount;
            }
            
            return count;
        }
    }
    
    public void AddCompletedCount()
    {
        CompletedCount++;
        // OnUpdate?.Invoke();
    }
    
    public bool IsCompleted => CompletedCount >= ToCompleteGoal;

    public int CompletedCountUI
    {
        get
        {
            int count = CompletedCount;
            
            if (ProgressLevel != -1)
            {
                for(int i = ProgressLevel; i > -1; i--)
                {
                    count -= Slots[i].ToCompleteCount;
                }
            }
            
            return count;
        }
    }
    public int ToCompleteGoalUI => Slots[ProgressLevel + 1].ToCompleteCount;

    public void Load()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].Load(this, i);
        }
        
        // OnUpdate?.Invoke();
    }

    public void Complete()
    {
        Slots[ProgressLevel + 1].Complete();
        // OnUpdate?.Invoke();
    }

    public void Reset()
    {
        ProgressLevel = 0;
        CompletedCount = 0;
        
        // OnUpdate?.Invoke();
    }

    public void Init()
    {
        // Debug.Log(Name + "ach loaded");
        Load();
    }
}
