using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class DataManager {
    private static int totalScore = 0, curLevel = 1;
    private static bool isGameStart = false;
    private static int totalLevel = 10;
    private static bool isCharacterInitiated = false;

    public static int TotalScore { get => totalScore; set => totalScore = value; }
    public static int CurLevel { get => curLevel; set => curLevel = value; }
    public static bool IsGameStart { get => isGameStart; set => isGameStart = value; }

    public static bool[] CharacterIsSelected = { false, false, false, false, false, false, false, false, false, false };

    public static int FinishedCharacters = 0;
    public static bool IsCharacterInitiated { get => IsCharacterInitiated; set => IsCharacterInitiated = value; }

    public static void ClearData() {
        totalScore = 0;
        curLevel = 1;
        isGameStart = false;
    }

    public static void ClearCharacterData()
    {
        for (int i = 0; i < totalLevel; i++)
        {
            CharacterIsSelected[i] = false;
        }

        FinishedCharacters = 0;

        Debug.Log("Character Cleared");
    }

    public static void AddCharacter()
    {
        FinishedCharacters++;

        Debug.Log("Character Added");
    }
}
