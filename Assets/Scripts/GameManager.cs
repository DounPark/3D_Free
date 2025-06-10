using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CurrentStage { get; private set; } = 1;
    
    // public int Gold { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // public void AddGold(int amount)
    // {
    //     Gold += amount;
    //     UIManager.Instance.UpdateGoldUI(Gold);
    // }
    //
    // public void SpendGold(int amount)
    // {
    //     Gold -= amount;
    //     UIManager.Instance.UpdateGoldUI(Gold);
    // }

    public void NextStage()
    {
        Debug.Log("[GameManager] NextStage 호출됨");
        StageManager.Instance.NextStage();
    }
}
