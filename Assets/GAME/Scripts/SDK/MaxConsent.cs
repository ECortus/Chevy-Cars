using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MaxConsent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text m_messageText;
    [SerializeField] private Button acceptButton;
    [SerializeField] private GameObject menu;

    [Space]
    public string TermsOfService_URL;
    public string PrivacyPolicy_URL;

    private void Awake()
    {
        if (!MaxSdk.HasUserConsent() || !MaxSdk.IsAgeRestrictedUser() || !MaxSdk.IsDoNotSell()
            || !MaxSdk.IsAgeRestrictedUserSet() || !MaxSdk.IsDoNotSellSet())
        {
            menu.SetActive(true);
            acceptButton.onClick.AddListener(Accept);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mousePos = new Vector3(eventData.position.x, eventData.position.y, 0);
        int linkId = TMP_TextUtilities.FindIntersectingLink(m_messageText, mousePos, eventData.pressEventCamera);
        if (linkId !=-1)
        {
            TMP_LinkInfo link = m_messageText.textInfo.linkInfo[linkId];
            if (link.GetLinkID() == "TOS")
            { 
                OpenLink(TermsOfService_URL);
            }
            else if (link.GetLinkID() == "PP")
            {
                OpenLink(PrivacyPolicy_URL);
            }
        }
    }
    
    private void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    private void Accept()
    {
        MaxSdk.SetHasUserConsent(true);
        MaxSdk.SetIsAgeRestrictedUser(false);
        MaxSdk.SetDoNotSell(false);
    }
}
