using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// [ExecuteInEditMode]
public class FortuneWheel : MonoBehaviour
{
    [SerializeField] private FortuneWheelSetup setup;
    private Lot[] Lots => setup.Lots;
    
    private float[] LotSectorAngleStart;
    private float[] LotSectorAngleSize;

    [Space] 
    [SerializeField] private Image lotPrefab;
    
    [Space] 
    [SerializeField] private Transform lotsParent;
    [SerializeField] private Button turnButton, closeButton;
    [SerializeField] private TextMeshProUGUI costText;
    
    [Space]
    [SerializeField] private GetLootUI getLoot;

    [Space] 
    [SerializeField] private Transform toRotate;
    [SerializeField] private float rotateSpeedMin;
    [SerializeField] private float rotateSpeedMax;
    [SerializeField] private float rotateTimeMin;
    [SerializeField] private float rotateTimeMax;

    [Space] 
    [SerializeField] private int Cost;

    [ContextMenu("Form wheel")]
    public void Form()
    {
        lotsParent.localEulerAngles = Vector3.zero;
        
        float full = 360f;
        float allRarity = 0f;

        int count = Lots.Length;

        foreach (var VARIABLE in Lots)
        {
            allRarity += VARIABLE.Rarity;
        }

        LotSectorAngleStart = new float[count];
        LotSectorAngleSize = new float[count];
        
        for(int i = 0; i < lotsParent.childCount; )
        {
            DestroyImmediate(lotsParent.GetChild(0).gameObject);
        }

        Image prefab = lotPrefab;

        float percent = 0f;
        float startAngle = 0f;
        float angle = 0f;
        Image imageInst;
        Transform instChild;

        for (int i = 0; i < count; i++)
        {
            percent = Lots[i].Rarity / allRarity;
            angle = full * percent;

            LotSectorAngleStart[i] = startAngle;
            LotSectorAngleSize[i] = angle;

            imageInst = Instantiate(prefab, lotsParent);
            imageInst.gameObject.SetActive(true);
            imageInst.color = setup.GetColor(i);
            imageInst.transform.localEulerAngles = new Vector3(0, 0, -startAngle);
            imageInst.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
            imageInst.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            
            imageInst.type = Image.Type.Filled;
            imageInst.fillAmount = percent;
            instChild = imageInst.transform.GetChild(0);
            instChild.localEulerAngles = new Vector3(0, 0, -angle / 2f);
            instChild.GetComponent<RectTransform>().sizeDelta = Vector3.one * 100f;

            instChild.GetComponentInChildren<TextMeshProUGUI>().text = Lots[i].Count.ToString();
            instChild.GetComponentInChildren<Image>().sprite = Lots[i].Prize.Sprite;
            
            startAngle += angle;
        }
        
        SetRandomStartAngle();
    }

    private Coroutine _coroutine;

    private void OnEnable()
    {
        SetRandomStartAngle();
        SetText();
        
        Form();
    }

    public void TRY_YOU_LUCK()
    {
        if (SoftCurrency.Value >= Cost)
        {
            SoftCurrency.Minus(Cost);
            _coroutine ??= StartCoroutine(Rotating());
        }
    }

    IEnumerator Rotating()
    {
        turnButton.interactable = false;
        closeButton.interactable = false;
        
        float angle = lotsParent.eulerAngles.z;
        float rotateSpeed = Random.Range(rotateSpeedMin, rotateSpeedMax);
        float fullTime = Random.Range(rotateTimeMin, rotateTimeMax);
        float time = fullTime;

        float deltaTime;
        
        while (time > 0f)
        {
            deltaTime = Time.deltaTime;
            
            toRotate.Rotate(new Vector3(0,0,1) * rotateSpeed * deltaTime * time / fullTime, Space.Self);
            angle += rotateSpeed * deltaTime * time / fullTime;
            
            time -= deltaTime;
            yield return null;
        }

        Prize[] prize = new Prize[1];
        angle = angle % 360f;

        for(int i = 0; i < Lots.Length; i++)
        {
            if (angle > LotSectorAngleStart[i] && angle <= LotSectorAngleSize[i] + LotSectorAngleStart[i])
            {
                prize[0] = Lots[i].Prize;
                prize[0].Count = Lots[i].Count;
                break;
            }
        }
        
        getLoot.Show(prize);
        yield return new WaitUntil(() => !getLoot.parent.gameObject.activeSelf);
        
        turnButton.interactable = true;
        closeButton.interactable = true;

        _coroutine = null;
        SetRandomStartAngle();
        
        yield return null;
    }

    void SetText()
    {
        costText.text = $"{Cost.ToString()}";
    }

    void SetRandomStartAngle()
    {
        lotsParent.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
    }
}
