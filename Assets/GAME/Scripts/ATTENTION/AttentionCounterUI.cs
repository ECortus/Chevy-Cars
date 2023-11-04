using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttentionCounterUI : Instancer<AttentionCounterUI>
{
    protected override void SetInstance()
    {
        Instance = this;
    }
    
    private int AttentionLevel => AttentionController.Instance.Attention;
    private int MaxLevel => AttentionController.Instance.MaxAttention;

    [SerializeField] private Image[] SpriteObjects;
    [SerializeField] private Sprite onSprite, offSprite;
 
    void Start()
    {
        Clean();
    }
    
    public void Refresh()
    {
        Clean();
        
        for (int i = 0; i < SpriteObjects.Length; i++)
        {
            if (i < AttentionLevel)
            {
                SpriteObjects[i].sprite = onSprite;
            }
            else
            {
                SpriteObjects[i].sprite = offSprite;
            }
        }
    }

    public void Clean()
    {
        for (int i = 0; i < SpriteObjects.Length; i++)
        {
            if (i < MaxLevel)
            {
                SpriteObjects[i].gameObject.SetActive(true);
                SpriteObjects[i].sprite = offSprite;
                
            }
            else
            {
                SpriteObjects[i].gameObject.SetActive(false);
            }
        }
    }
}
