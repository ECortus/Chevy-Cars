using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreAnim : MonoBehaviour
{
    [Space]
    [SerializeField] private float height = 3f;
    [Range(0f, 0.4f)]
    [SerializeField] private float preTimeBottom = 0.1f;
    [Range(0f, 0.4f)]
    [SerializeField] private float preTimeTop = 0.1f;
    [SerializeField] private float time = 1f;

    [SerializeField] private TextMeshProUGUI text;

    public bool Active => gameObject.activeSelf;
    
    public async void On(Vector3 pos, int score)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        transform.rotation = Camera.main.transform.rotation;
        transform.localScale = Vector3.zero;

        text.text = $"+{score.ToString()}";

        transform.DOKill();
        
        float y = height * preTimeBottom;
        transform.DOScale(Vector3.one * 0.01f, time * preTimeBottom);
        await transform.DOMove(transform.position + new Vector3(0, y, 0), time * preTimeBottom).AsyncWaitForCompletion();

        y = height - height * (preTimeBottom + preTimeTop);
        await transform.DOMove(transform.position + new Vector3(0, y, 0), time - time * (preTimeBottom + preTimeTop)).AsyncWaitForCompletion();
        
        y = height * preTimeTop;
        transform.DOScale(Vector3.zero, time * preTimeTop);
        await transform.DOMove(transform.position + new Vector3(0, y, 0), time * preTimeTop).AsyncWaitForCompletion();
        
        if (Active) Off();
    }

    private void OnDestroy()
    {
        Off();
    }

    public void Off()
    {
        transform.DOKill();
        gameObject.SetActive(false);
    }
}
