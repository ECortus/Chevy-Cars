using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootBoxes/Create action collection")]
public class LootBoxesActions : ScriptableObject
{
    [SerializeField] private PuzzlePeaces peaces;
    
    public void PlusNormalPuzzle(int count)
    {
        peaces.GetPuzzleByType(PuzzlePeaceType.Normal).Plus(count);
    }
    
    public void PlusRarePuzzle(int count)
    {
        peaces.GetPuzzleByType(PuzzlePeaceType.Rare).Plus(count);
    }
    
    public void PlusUniquePuzzle(int count)
    {
        peaces.GetPuzzleByType(PuzzlePeaceType.Unique).Plus(count);
    }
}
