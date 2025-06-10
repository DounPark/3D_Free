using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public int currentStage = 1;

    private void Awake()
    {
        Instance = this;
    }

    public void NextStage()
    {
        currentStage++;
        Debug.Log($"StageManager: currentStage={currentStage}");
        
        UIManager.Instance.UpdateStageUI(currentStage);
        // 절차적 맵 생성
        MapManager.Instance.GenerateStageMap(currentStage);
        // 웨이브 시작
        WaveManager.Instance.StartWaveExternally();
    }
}
