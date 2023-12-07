using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCreatedAwakeObjects : MonoBehaviour
{
    void Awake()
    {
        Object[] objAch = Resources.LoadAll("ACHIEVEMENTS", typeof(AchievementObject));
        foreach (var VARIABLE in objAch)
        {
            ((AchievementObject)VARIABLE).Init();
        }
        
        Object objPuzzle = Resources.Load("PUZZLES/PuzzleCollection", typeof(PuzzlePeaces));
        ((PuzzlePeaces)objPuzzle).Init();
    }
}
