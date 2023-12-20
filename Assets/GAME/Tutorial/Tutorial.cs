using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; private set; }   
    
    public static bool Completed
    {
        get => PlayerPrefs.GetInt("TutorialCompleted", 0) != 0;
        private set
        {
            PlayerPrefs.SetInt("TutorialCompleted", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    [SerializeField] private GameObject driveObj;
    [SerializeField] private Animator handAnim;

    [Inject] private void Awake()
    {
        if (Completed)
        {
            gameObject.SetActive(false);
            return;
        }

        Instance = this;
    }

    public async void On()
    {
        if (Completed)
        {
            gameObject.SetActive(false);
            return;
        }
        
        driveObj.SetActive(true);

        await UniTask.WaitUntil(() => Input.GetMouseButton(0));

        Completed = true;
        gameObject.SetActive(false);
    }
}
