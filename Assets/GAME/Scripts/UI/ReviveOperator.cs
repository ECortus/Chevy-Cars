using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReviveOperator : Instancer<ReviveOperator>
{
    protected override void SetInstance()
    {
        Instance = this;
    }
    
    [SerializeField] private GameObject menu;
    [SerializeField] private Transform reviveParent;
    
    [Space]
    [SerializeField] private Slider slider;
    [SerializeField] private Transform noThanks;
    [SerializeField] private float noThanksTime = 3;

    public async void On()
    {
        CameraController.Instance.SetDistance(11f);
        
        Hide();
        
        menu.SetActive(true);
        time = 0;
        
        slider.minValue = 0;
        slider.maxValue = noThanksTime;
        slider.gameObject.SetActive(true);
        update = true;
        
        GameManager.Instance.SetTimeScale(0f);
        
        await reviveParent.DOScale(Vector3.one, 0.25f).SetUpdate(true).AsyncWaitForCompletion();
    }

    public void Revive()
    {
        menu.SetActive(false);
        
        CopArrestController.Reset();
        GameManager.Instance.SetTimeScale(1f);
        
        CopsPool.Instance.KillAllOnDistanceFromTarget(PlayerController.Instance.Transform, 10f);
        
        CameraController.Instance.ResetDistance();
        PlayerController.Instance.OnWithoutRotation(PlayerController.Instance.Transform.position);
    }

    private float time = 0;
    private bool update = false;

    void Update()
    {
        if (update)
        {
            if (time < noThanksTime)
            {
                time += Time.unscaledDeltaTime;
                slider.value = time;
            }
            else
            {
                NoThanksScale();
                slider.gameObject.SetActive(false);
            }
        }
    }

    void NoThanksScale()
    {
        update = false;
        noThanks.DOScale(Vector3.one, 0.35f).SetUpdate(true);
    }
    
    void Hide()
    {
        menu.SetActive(false);
        reviveParent.localScale = Vector3.zero;
        noThanks.localScale = Vector3.zero;
        slider.gameObject.SetActive(false);
    }

    public void Decline()
    {
        menu.SetActive(false);
        LevelManager.Instance.Lose();
    }
}
