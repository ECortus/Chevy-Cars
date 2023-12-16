using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounterUI : MonoBehaviour
{
    private TextMeshProUGUI _counter;
    
    private void OnEnable()
    {
        Score.OnUpdate += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        Score.OnUpdate -= Refresh;
    }

    private void Refresh()
    {
        if (!_counter) _counter = GetComponent<TextMeshProUGUI>();

        _counter.text = $"SCORE: {Score.Value.ToString()}";
    }
}
