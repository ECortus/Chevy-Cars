using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class LobbyTutorial : MonoBehaviour
{
    public static bool Completed
    {
        get => PlayerPrefs.GetInt("LobbyTutorialCompleted", 0) != 0;
        private set
        {
            PlayerPrefs.SetInt("LobbyTutorialCompleted", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    [SerializeField] private AchievementObject achievementLevelComplete;
    [SerializeField] private GameObject achievementMenu;
    [SerializeField] private GameObject tapAchievementMenu, tapGetAchievementReward;
    [SerializeField] private Button[] buttonsToOff;

    [Inject] private void Awake()
    {
        if (Completed)
        {
            gameObject.SetActive(false);
        }
    }

    public void CheckAchievementTutorial()
    {
        if (Completed)
        {
            gameObject.SetActive(false);
            return;
        }
        
        if (achievementLevelComplete.ProgressLevel < 0 && achievementLevelComplete.IsCompleted)
        {
            OnAchievementTutorial();
        }
    }

    public async void OnAchievementTutorial()
    {
        if (Completed)
        {
            gameObject.SetActive(false);
            return;
        }

        foreach (var VARIABLE in buttonsToOff)
        {
            VARIABLE.interactable = false;
        }

        SlideMenuUI.Block = true;
        
        tapAchievementMenu.SetActive(true);

        await UniTask.WaitUntil(() => achievementMenu.activeSelf);
        
        tapAchievementMenu.SetActive(false);
        tapGetAchievementReward.SetActive(true);

        await UniTask.WaitUntil(() => achievementLevelComplete.ProgressLevel > -1);
        
        tapGetAchievementReward.SetActive(false);
        
        foreach (var VARIABLE in buttonsToOff)
        {
            VARIABLE.interactable = true;
        }
        
        Completed = true;
        gameObject.SetActive(false);

        SlideMenuUI.Block = false;
    }
}
