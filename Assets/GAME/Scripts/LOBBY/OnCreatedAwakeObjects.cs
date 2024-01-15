using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class OnCreatedAwakeObjects : MonoBehaviour
{
    [SerializeField] private LobbyTutorial lobby;
    
    async void Awake()
    {
        Object[] objAch = Resources.LoadAll("ACHIEVEMENTS", typeof(AchievementObject));
        foreach (var VARIABLE in objAch)
        {
            ((AchievementObject)VARIABLE).Init();
        }
        
        Object objPuzzle = Resources.Load("PUZZLES/PuzzleCollection", typeof(PuzzlePeaces));
        ((PuzzlePeaces)objPuzzle).Init();

        await UniTask.Delay(100);
        
        lobby.CheckAchievementTutorial();
    }
}
