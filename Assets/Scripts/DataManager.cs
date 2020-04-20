using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager {
    private static int totalScore = 0, curLevel = 1;
    private static bool isGameStart = false;

    public static int TotalScore { get => totalScore; set => totalScore = value; }
    public static int CurLevel { get => curLevel; set => curLevel = value; }
    public static bool IsGameStart { get => isGameStart; set => isGameStart = value; }

    public static void ClearData() {
        totalScore = 0;
        curLevel = 1;
        isGameStart = false;
    }
}
