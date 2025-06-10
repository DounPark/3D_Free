using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI waveText;
    
    public Button moveSpeedButton;
    public Button attackPowerButton;
    public Button attackSpeedButton;

    private int moveCost = 10;
    private int powerCost = 20;
    private int speedCost = 20;
    

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        moveSpeedButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.Gold >= moveCost)
            {
                GameManager.Instance.SpendGold(moveCost);
                Player.Instance.UpgradeMoveSpeed();
                moveCost += 5;
            }
        });
        
        attackPowerButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.Gold >= powerCost)
            {
                GameManager.Instance.SpendGold(powerCost);
                Player.Instance.UpgradeAttackPower();
                powerCost += 10;
            }
        });
        
        attackSpeedButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.Gold >= speedCost)
            {
                GameManager.Instance.SpendGold(speedCost);
                Player.Instance.UpgradeAttackSpeed();
                speedCost += 10;
            }
        });
    }

    public void UpdateGoldUI(int amount)
    {
        goldText.text = $"Gold: {amount}";
    }

    public void UpdateWaveUI(int wave)
    {
        waveText.text = $"Wave: {wave}";
    }
}
