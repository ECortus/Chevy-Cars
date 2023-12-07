using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetLootUI : MonoBehaviour
{
    public GameObject parent;
    [SerializeField] private Transform grid;
    [SerializeField] private Transform buttonTransform;
    
    [Space]
    [SerializeField] private GameObject cellPrefab;

    private Prize[] prizes;
    
    public async void Show(Prize[] prs)
    {
        SlideMenuUI.Block = true;

        prizes = new Prize[prs.Length];

        for (int i = 0; i < prs.Length; i++)
        {
            prizes[i] = prs[i];
        }

        parent.transform.DOKill();
        
        parent.gameObject.SetActive(true);
        parent.transform.localScale = Vector3.zero;
        buttonTransform.localScale = Vector3.zero;

        foreach (Transform VARIABLE in grid)
        {
            VARIABLE.DOKill();
            Destroy(VARIABLE.gameObject);
        }
        
        Debug.Log("showed");
        await parent.transform.DOScale(Vector3.one, 0.35f).AsyncWaitForCompletion();

        Transform[] cells = new Transform[prizes.Length];
        Transform cell;
        
        for(int i = 0; i < prizes.Length; i++)
        {
            cell = Instantiate(cellPrefab).GetComponent<Transform>();
            cell.gameObject.SetActive(true);
            
            cell.SetParent(grid);
            cell.localScale = Vector3.zero;
            cell.localPosition = new Vector3(cell.localPosition.x, cell.localPosition.y, 0f);

            cell.GetComponent<Image>().sprite = prizes[i].Sprite;
            cell.GetComponentInChildren<TextMeshProUGUI>().text = $"x {prizes[i].Count.ToString()}";

            cells[i] = cell;
        }

        for(int i = 0; i < cells.Length; i++)
        {
            await cells[i].DOScale(Vector3.one, 0.5f).AsyncWaitForCompletion();
        }

        buttonTransform.DOScale(Vector3.one, 0.5f);
    }

    public async void OnGetButtonClick()
    {
        foreach (var VARIABLE in prizes)
        {
            VARIABLE.Get();
        }
        
        await parent.transform.DOScale(Vector3.zero, 0.35f).AsyncWaitForCompletion();
        
        parent.SetActive(false);
        SlideMenuUI.Block = false;
    }
}
