using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ThrowingSpritesController : MonoBehaviour
{
    private static ThrowingSpritesController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            Pool[i].transform.DOKill();
        }
    }
    
    [SerializeField] private Transform parent;

    [Space] 
    [SerializeField] private float startScaleSize = 0.2f;
    [SerializeField] private float endScaleSize = 1.25f;
    public float Duration = 0.75f;
    [Range(0f, 1f)] public float PercentToTop = 0.15f;
    
    [Space]
    [SerializeField] private List<ThrowingSprite> Prefabs;

    private ThrowingSprite GetPrefabByType(ThrowingSpriteAnimationType type)
    {
        for (int i = 0; i < Prefabs.Count; i++)
        {
            if (Prefabs[i].Type == type)
            {
                return Prefabs[i];
            }
        }

        return null;
    }

    private List<ThrowingSprite> Pool = new List<ThrowingSprite>();

    [Space] 
    [SerializeField] private bool testOn = false;
    [SerializeField] private ThrowingSpriteAnimationType debugType;
    private void Update()
    {
        if (testOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Play(debugType);
            }
        }
    }

    public static void Play(ThrowingSpriteAnimationType type)
    {
        Instance._Play(type);
    }

    private async void _Play(ThrowingSpriteAnimationType type)
    {
        if (type == ThrowingSpriteAnimationType.Default) return;
        
        ThrowingSprite toPlay = null;
        if (Pool.Count > 0)
        {
            for (int i = 0; i < Pool.Count; i++)
            {
                if (Pool[i] && Pool[i].Type == type && !Pool[i].Playing)
                {
                    toPlay = Pool[i];
                    break;
                }
            }
        }
        
        if (!toPlay)
        {
            toPlay = Instantiate(GetPrefabByType(type), parent);
            Pool.Add(toPlay);
        }

        toPlay.Playing = true;
        toPlay.gameObject.SetActive(true);

        Vector3 startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 endPosition = toPlay.EndPosition.position;
        startPosition.z = endPosition.z;
        Vector3 middlePosition = startPosition + (endPosition - startPosition) * PercentToTop;
        
        // Debug.Log("start - " + startPosition + ", middle - " + middlePosition + ", end - " + endPosition);

        Vector3 startScale = Vector3.one * startScaleSize;
        Vector3 endScale = Vector3.one * endScaleSize;

        float duration = Duration;
        
        float startTime = duration * PercentToTop;
        float endTime = duration * (1f - PercentToTop);
        
        toPlay.transform.localScale = startScale;
        toPlay.transform.position = startPosition;

        Ease ease = Ease.Linear;

        toPlay.transform.DOMove(middlePosition, startTime).SetEase(ease);
        toPlay.transform.DOScale(endScale, startTime).SetEase(ease);

        await UniTask.Delay((int)(startTime * 1000f));

        toPlay.transform.DOMove(endPosition, endTime).SetEase(ease);
        toPlay.transform.DOScale(startScale, endTime).SetEase(ease);
        
        await UniTask.Delay((int)(endTime * 1000f));

        toPlay.transform.DOKill();
        
        toPlay.Playing = false;
        toPlay.gameObject.SetActive(false);
    }
}
