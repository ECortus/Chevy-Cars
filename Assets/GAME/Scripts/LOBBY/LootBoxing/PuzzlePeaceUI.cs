using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePeaceUI : MonoBehaviour
{
    [SerializeField] private PuzzlePeaces collection;
    [SerializeField] private PuzzlePeaceType Type;

    [Space] 
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Image imageSprite;
    
    private void Awake()
    {
        // collection.OnUpdate += Refresh;
        PuzzlePeaces.OnUpdate += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        // collection.OnUpdate -= Refresh;
        PuzzlePeaces.OnUpdate -= Refresh;
    }

    private void Refresh()
    {
        // Debug.Log("update puzzle " + Type);
        imageSprite.sprite = collection.GetPuzzleByType(Type).Sprite;
        countText.text = $"{collection.GetPuzzleByType(Type).Value.ToString()}";
    }
}
