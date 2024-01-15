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
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        gameObject.SetActive(true);
        _coroutine = StartCoroutine(Anim(pos, score));
    }

    private Coroutine _coroutine;

    IEnumerator Anim(Vector3 pos, int score)
    {
        transform.position = pos;
        transform.rotation = Camera.main.transform.rotation;
        transform.localScale = Vector3.zero;

        text.text = $"+{score.ToString()}";

        transform.DOKill();
        
        float y = height * preTimeBottom;
        transform.DOScale(Vector3.one * 0.01f, time * preTimeBottom);
        transform.DOMove(transform.position + new Vector3(0, y, 0), time * preTimeBottom);
        
        yield return new WaitForSeconds(time * preTimeBottom);

        y = height - height * (preTimeBottom + preTimeTop);
        transform.DOMove(transform.position + new Vector3(0, y, 0),time - time * (preTimeBottom + preTimeTop));
        
        yield return new WaitForSeconds(time - time * (preTimeBottom + preTimeTop));
        
        y = height * preTimeTop;
        transform.DOScale(Vector3.zero, time * preTimeTop);
        transform.DOMove(transform.position + new Vector3(0, y, 0), time * preTimeTop);
        
        yield return new WaitForSeconds(time * preTimeTop);
        
        if (Active) Off();
    }

    private void OnDestroy()
    {
        Off();
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public void Off()
    {
        transform.DOKill();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        
        gameObject.SetActive(false);
    }
}
