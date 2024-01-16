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
    [SerializeField] private Button settings, turnButton, turnFreeButton, turnAdButton, closeButton;
    [SerializeField] private GameObject notAvailableButton;
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

    [Space] 
    [SerializeField] private TextMeshProUGUI timer;

    private void Awake()
    {
        SoftCurrency.OnUpdate += SetButton;
        SetButton();

        time = TimeInSeconds();
        if (time - SpinningTime > 6 * 60 * 60)
        {
            FreeSpin = true;
            AdSpinningCount = 5;

            SpinningTime = time;
        }
        
        RefreshButtons();
    }

    private void OnDestroy()
    {
        SoftCurrency.OnUpdate -= SetButton;
    }
    
    float TimeInSeconds()
    {
        DateTime epochStart = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        float timestamp = (float)((DateTime.UtcNow - epochStart).TotalSeconds);

        return timestamp;
    }

    private void SetButton()
    {
        if (_coroutine != null || SoftCurrency.Value < Cost)
        {
            turnButton.interactable = false;
        }
        else
        {
            turnButton.interactable = true;
        }
    }

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
        
        RefreshButtons();
    }

    private Coroutine _coroutine;

    private void OnEnable()
    {
        SetRandomStartAngle();
        SetText();
        
        Form();
    }

    private float SpinningTime
    {
        get => PlayerPrefs.GetFloat("Fortune_SpinningTime", 0);
        set
        {
            PlayerPrefs.SetFloat("Fortune_SpinningTime", value);
            PlayerPrefs.Save();
        }
    }

    private bool FreeSpin
    {
        get => PlayerPrefs.GetInt("Fortune_FreeSpin", 0) > 0;
        set
        {
            PlayerPrefs.SetInt("Fortune_FreeSpin", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    private int AdSpinningCount
    {
        get => PlayerPrefs.GetInt("Fortune_AdSpinningCount", 0);
        set
        {
            PlayerPrefs.SetInt("Fortune_AdSpinningCount", value);
            PlayerPrefs.Save();
        }
    }

    public void TRY_YOU_LUCK()
    {
        if (SoftCurrency.Value >= Cost)
        {
            SoftCurrency.Minus(Cost);
            _coroutine ??= StartCoroutine(Rotating());
        }
    }
    
    public void TRY_YOU_LUCK_FREE()
    {
        if (FreeSpin)
        {
            FreeSpin = false;
            SpinningTime = TimeInSeconds();
            
            _coroutine ??= StartCoroutine(Rotating());
        }
    }

    public void TRY_YOU_LUCK_AD()
    {
        if (AdSpinningCount > 0)
        {
            // ad and check no ads
            
            AdSpinningCount--;
            SpinningTime = TimeInSeconds();
            
            _coroutine ??= StartCoroutine(Rotating());
        }
    }
    
    IEnumerator Rotating()
    {
        // turnButton.interactable = false;
        settings.interactable = false;
        turnFreeButton.interactable = false;
        turnAdButton.interactable = false;
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
                prize[0].Number = Lots[i].Count;
                break;
            }
        }
        
        getLoot.Show(prize);
        yield return new WaitUntil(() => !getLoot.parent.gameObject.activeSelf);
        
        // turnButton.interactable = true;
        turnFreeButton.interactable = FreeSpin;
        turnAdButton.interactable = !FreeSpin && AdSpinningCount > 0;
        settings.interactable = true;
        closeButton.interactable = true;

        _coroutine = null;
        SetRandomStartAngle();
        
        RefreshButtons();
        
        yield return null;
    }

    private float time;
    private string seconds, minutes, hours;
    
    void Update()
    {
        if (!FreeSpin && AdSpinningCount == 0)
        {
            if (timer.gameObject.activeSelf)
            {
                time = (SpinningTime + 6 * 60 * 60) - TimeInSeconds();
                seconds = $"{(int)time % 60}";
                minutes = $"{(int)(time - ((int)time / 3600) * 3600) / 60}";
                hours = $"{(int)time / 3600}";
            
                seconds = seconds.Length < 2 ? "0" + seconds : seconds;
                minutes = minutes.Length < 2 ? "0" + minutes : minutes;
                hours = hours.Length < 2 ? "0" + hours : hours;
            
                timer.text = $"{hours}:{minutes}:{seconds}";

                if (time <= 0)
                {
                    FreeSpin = true;
                    AdSpinningCount = 5;

                    SpinningTime = time;
                    RefreshButtons();
                }
            }
        }
    }

    void SetText()
    {
        costText.text = $"{Cost.ToString()}";
    }

    void RefreshButtons()
    {
        turnButton.gameObject.SetActive(false);
        turnFreeButton.gameObject.SetActive(FreeSpin);
        turnAdButton.gameObject.SetActive(!FreeSpin && AdSpinningCount > 0);
        notAvailableButton.gameObject.SetActive(!FreeSpin && AdSpinningCount == 0);
        timer.gameObject.SetActive(!FreeSpin && AdSpinningCount == 0);
    }

    void SetRandomStartAngle()
    {
        lotsParent.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
    }
}
