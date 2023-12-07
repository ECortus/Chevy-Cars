using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TypedCurrency
{
    private static Sprite soft, hard;
    private static PuzzlePeaces puzzles;
    
    public enum Currency
    {
        Soft, Hard, NormalPuzzle, RarePuzzle, UniquePuzzle
    }

    public static int Value(Currency type)
    {
        if (!puzzles) puzzles = Resources.Load("PUZZLES/PuzzleCollection") as PuzzlePeaces;
            
        switch(type)
        {
            case Currency.Soft:
                return SoftCurrency.Value;
            case Currency.Hard:
                return HardCurrency.Value;
            case Currency.NormalPuzzle:
                return puzzles.GetPuzzleByType(PuzzlePeaceType.Normal).Value;
            case Currency.RarePuzzle:
                return puzzles.GetPuzzleByType(PuzzlePeaceType.Rare).Value;
            case Currency.UniquePuzzle:
                return puzzles.GetPuzzleByType(PuzzlePeaceType.Unique).Value;
        }

        return -1;
    }
    
    public static Sprite Sprite(Currency type)
    {
        if (!puzzles) puzzles = Resources.Load("PUZZLES/PuzzleCollection") as PuzzlePeaces;
        if (!soft) soft = Resources.Load<Sprite>("CURRENCY/SOFT");
        if (!hard) hard = Resources.Load<Sprite>("CURRENCY/HARD");
            
        switch(type)
        {
            case Currency.Soft:
                return soft;
            case Currency.Hard:
                return hard;
            case Currency.NormalPuzzle:
                return puzzles.GetPuzzleByType(PuzzlePeaceType.Normal).Sprite;
            case Currency.RarePuzzle:
                return puzzles.GetPuzzleByType(PuzzlePeaceType.Rare).Sprite;
            case Currency.UniquePuzzle:
                return puzzles.GetPuzzleByType(PuzzlePeaceType.Unique).Sprite;
        }
            
        return null;
    }
    
    public static void Plus(Currency type, int amount)
    {
        if (!puzzles) puzzles = Resources.Load("PUZZLES/PuzzleCollection") as PuzzlePeaces;
            
        switch(type)
        {
            case Currency.Soft:
                SoftCurrency.Plus(amount);
                break;
            case Currency.Hard:
                HardCurrency.Plus(amount);
                break;
            case Currency.NormalPuzzle:
                puzzles.GetPuzzleByType(PuzzlePeaceType.Normal).Plus(amount);
                break;
            case Currency.RarePuzzle:
                puzzles.GetPuzzleByType(PuzzlePeaceType.Rare).Plus(amount);
                break;
            case Currency.UniquePuzzle:
                puzzles.GetPuzzleByType(PuzzlePeaceType.Unique).Plus(amount);
                break;
        }
    }
    
    public static void Minus(Currency type, int amount)
    {
        if (!puzzles) puzzles = Resources.Load("PUZZLES/PuzzleCollection") as PuzzlePeaces;
            
        switch(type)
        {
            case Currency.Soft:
                SoftCurrency.Minus(amount);
                break;
            case Currency.Hard:
                HardCurrency.Minus(amount);
                break;
            case Currency.NormalPuzzle:
                puzzles.GetPuzzleByType(PuzzlePeaceType.Normal).Minus(amount);
                break;
            case Currency.RarePuzzle:
                puzzles.GetPuzzleByType(PuzzlePeaceType.Rare).Minus(amount);
                break;
            case Currency.UniquePuzzle:
                puzzles.GetPuzzleByType(PuzzlePeaceType.Unique).Minus(amount);
                break;
        }
    }
}
