using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelsCompleteSlider : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup icons;
        [SerializeField] private GameObject iconPrefab;
    
        [Space] 
        [SerializeField] private List<Transform> toggles;
        
        [Space]
        [SerializeField] private Slider lvlSlider;
        [SerializeField] private int[] milestones;

        private void OnEnable()
        {
            Refresh();
        }

        [ContextMenu("Create")]
        public void Create()
        {
            for(int i = 0; i < icons.transform.childCount;)
            {
                DestroyImmediate(icons.transform.GetChild(0).gameObject);
            }
    
            int count = milestones.Length;
            if (count == 0) return;
    
            GameObject icon;
            float stat;
            
            toggles.Clear();
            
            for (int i = 0; i < count; i++)
            {
                icon = Instantiate(iconPrefab, icons.transform);
                stat = milestones[i];
                
                SetIcon(icon, stat, false);
            }
    
            RectTransform flySliderTransform = lvlSlider.GetComponent<RectTransform>();
            flySliderTransform.sizeDelta = 
                new Vector2(icons.cellSize.x * (count - 1) + (icons.spacing.x + 5) * (count - 2), flySliderTransform.sizeDelta.y);
        }
        
        void SetIcon(GameObject icon, float length, bool state)
        {
            TextMeshProUGUI text = icon.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            Transform toggle = icon.transform.GetChild(0);
    
            text.text = $"{Mathf.RoundToInt(length).ToString()}";
            
            toggle.GetChild(0).gameObject.SetActive(!state);
            toggle.GetChild(1).gameObject.SetActive(state);
            
            toggles.Add(toggle);
            
            icon.gameObject.SetActive(true);
        }
    
        void SetToggle(int index, bool state)
        {
            Transform toggle = toggles[index];
            toggle.GetChild(0).gameObject.SetActive(!state);
            toggle.GetChild(1).gameObject.SetActive(state);
        }
        
        public void Refresh()
        {
            lvlSlider.minValue = 0;
            lvlSlider.maxValue = 1;
    
            float count = milestones.Length;
            float space = 1f / count;
            
            int completed = 0;
            int requireLvl = 0;
            
            int lvl = Statistics.LevelIndex;
    
            for(int i = 0; i < count; i++)
            {
                requireLvl = milestones[i];
                
                if (lvl < requireLvl)
                {
                    if (i > 0)
                    {
                        requireLvl = milestones[i] - milestones[i - 1];
                        lvl -= milestones[i - 1];
                    }
                    break;
                }
                
                completed++;
            }
    
            for (int i = 0; i < count; i++)
            {
                SetToggle(i, i < completed);
            }
    
            if (completed == 0) lvlSlider.value = 0;
            else lvlSlider.value = space * (completed - 1) + space * ((float)lvl / (float)requireLvl);
        }
}
