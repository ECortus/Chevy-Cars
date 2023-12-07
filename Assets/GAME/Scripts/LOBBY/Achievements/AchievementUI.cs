using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] private AchievementObject achievement;

    [Space] 
    [SerializeField] private Image spriteShow;
    
    [Space] 
    [SerializeField] private GameObject fullCompleted;
    
    [Space]
    [SerializeField] private GameObject getReward;
    [SerializeField] private Image rewardSprite;
    [SerializeField] private TextMeshProUGUI rewardCountText;
    
    [Space]
    [SerializeField] private GameObject availableToComplete;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI goalText;

    private Button button;

    private void OnEnable()
    {
        // achievement.OnUpdate += Refresh;

        // button = getReward.GetComponent<Button>();
        // button.onClick.AddListener(OnButtonClick);
        
        Refresh();
    }

    public void OnButtonClick()
    {
        if (achievement.IsCompleted)
        {
            achievement.Complete();
        }
        
        Refresh();
    }

    private AchievementObject.RewardSlot slot;
    
    private void Refresh()
    {
        spriteShow.sprite = achievement.Sprite;
        
        if (achievement.IsMax)
        {
            ChangeObject(0);
            goalText.text = "COMPLETE";
        }
        else if (achievement.IsCompleted)
        {
            ChangeObject(1);

            slot = achievement.GetCurrentSlot();
            rewardSprite.sprite = slot.Prize.Sprite;
            rewardCountText.text = $"X {slot.PrizeCount.ToString()}";
        }
        else
        {
            ChangeObject(2);
            
            goalText.text = $"{achievement.CompletedCountUI.ToString()}/{achievement.ToCompleteGoalUI.ToString()}";
            progressSlider.minValue = 0;
            progressSlider.maxValue = achievement.ToCompleteGoalUI;
            progressSlider.value = achievement.CompletedCountUI;
        }
    }

    void ChangeObject(int index)
    {
        fullCompleted.SetActive(index == 0);
        getReward.SetActive(index == 1);
        availableToComplete.SetActive(index == 2);
        
        goalText.gameObject.SetActive(index == 0 || index == 2);
    }
}
