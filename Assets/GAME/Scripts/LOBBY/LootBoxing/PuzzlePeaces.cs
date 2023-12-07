using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Puzzles/Create peaces collection")]
public class PuzzlePeaces : ScriptableObject
{
    public static Action OnUpdate { get; set; }
    
    [field: SerializeField] public PuzzlePeace[] Puzzles { get; private set; }
    
    [Serializable]
    public struct PuzzlePeace
    {
        public PuzzlePeaceType Type;
        public Prize Prize;
    
        public Sprite Sprite => Prize.Sprite;

        public int Value
        {
            get => PlayerPrefs.GetInt(Type + "_PuzzlePeace", 0);
            set
            {
                PlayerPrefs.SetInt(Type + "_PuzzlePeace", value);
                PlayerPrefs.Save();
            }
        }

        private PuzzlePeaces _collection;

        // public void Load(PuzzlePeaces col)
        // {
        //     _collection = col;
        // }

        public void Plus(int vl)
        {
            Value += vl;
            OnUpdate?.Invoke();
        }
        
        public void Minus(int vl)
        {
            Value -= vl;
            OnUpdate?.Invoke();
        }
    }

    public PuzzlePeace GetPuzzleByType(PuzzlePeaceType type)
    {
        foreach (var VARIABLE in Puzzles)
        {
            if (VARIABLE.Type == type)
            {
                return VARIABLE;
            }
        }

        return new PuzzlePeace();
    }

    public void Init()
    {
        // foreach (var VARIABLE in Puzzles)
        // {
        //     VARIABLE.Load(this);
        // }
    }
}

public enum PuzzlePeaceType
{
    Normal, Rare, Unique
}
