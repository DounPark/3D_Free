using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("UI")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI maxStageText;
    public TextMeshProUGUI waveText;

    public Button abilityButton;
    public Button shopButton;
    public Button blacksmithButton;
    
    [Header("Player Panel")]
    public Button playerButton;
    public GameObject playerPanel;
    public TextMeshProUGUI statText;
    
    public Button closePlayerButton;
    
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private Transform equipGrid;
    [SerializeField] private Image[] equipSlotImages; // Head, Body, Weapon, Accessory 등

    [Header("Ability Panel")]
    public GameObject abilityPanel;
    public TextMeshProUGUI moveSpeedInfo;
    public TextMeshProUGUI attackPowerInfo;
    public TextMeshProUGUI attackSpeedInfo;
    public Button moveSpeedButton;
    public Button attackPowerButton;
    public Button attackSpeedButton;
    
    public Button closeAbilityButton;
    
    
    public PlayerData playerData;
    // public InventorySlotUI[] inventorySlots;

    private int moveCost = 10;
    private int powerCost = 20;
    private int speedCost = 20;
    
    int selectedStage = 1;
    int maxStage = 1;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 데이터 불러오기
        maxStage = PlayerPrefs.GetInt("MaxStage", 1);
        selectedStage = maxStage;

        UpdateUI();

        abilityButton.onClick.AddListener(() => abilityPanel.SetActive(true));
        shopButton.onClick.AddListener(() => Debug.Log("상점 - 추후 구현"));
        blacksmithButton.onClick.AddListener(() => Debug.Log("공방 - 추후 구현"));

        // 어빌리티 패널 버튼
        moveSpeedButton.onClick.AddListener(() => TryUpgrade("Move"));
        attackPowerButton.onClick.AddListener(() => TryUpgrade("Power"));
        attackSpeedButton.onClick.AddListener(() => TryUpgrade("Speed"));
        closeAbilityButton.onClick.AddListener(() => { abilityPanel.SetActive(false); UpdateUI(); });

        abilityPanel.SetActive(false);
        UpdateAbilityPanel();
        UpdateUI();
        
        playerButton.onClick.AddListener(() => { UpdatePlayerPanel(); playerPanel.SetActive(true); });
        closePlayerButton.onClick.AddListener(() => { UpdatePlayerPanel(); playerPanel.SetActive(false); });
    }

    void UpdateUI()
    {
        int currentGold = Player.Instance.Gold;
        goldText.text = $"Gold: {currentGold}";
        maxStageText.text = $"최고 스테이지: {maxStage}";

        // 스테이터스
        int moveSpeedLv = PlayerPrefs.GetInt("Upgrade_MoveSpeed", 0);
        int attackPowerLv = PlayerPrefs.GetInt("Upgrade_AttackPower", 0);
        int attackSpeedLv = PlayerPrefs.GetInt("Upgrade_AttackSpeed", 0);

        statText.text = $"이동속도: {3f + moveSpeedLv * 0.5f}\n" +
                         $"공격력: {5f + attackPowerLv * 1f}\n" +
                         $"공격속도: {1f - attackSpeedLv * 0.1f}";
    }
    
    void TryUpgrade(string type)
    {
        switch (type)
        {
            case "Move":
                if (Player.Instance.HasEnoughGold(moveCost))
                {
                    Player.Instance.SpendGold(moveCost);
                    Player.Instance.UpgradeMoveSpeed();
                    moveCost += 5;
                }
                break;
            case "Power":
                if (Player.Instance.HasEnoughGold(powerCost))
                {
                    Player.Instance.SpendGold(powerCost);
                    Player.Instance.UpgradeAttackPower();
                    powerCost += 10;
                }
                break;
            case "Speed":
                if (Player.Instance.HasEnoughGold(speedCost))
                {
                    Player.Instance.SpendGold(speedCost);
                    Player.Instance.UpgradeAttackSpeed();
                    speedCost += 10;
                }
                break;
        }
        UpdateAbilityPanel();
        UpdateUI();
    }

    void UpdateAbilityPanel()
    {
        var p = Player.Instance;
        moveSpeedInfo.text = $"이동속도 (+0.5) / Lv.{p.MoveSpeedLevel} / 비용: {moveCost}";
        attackPowerInfo.text = $"공격력 (+1) / Lv.{p.AttackPowerLevel} / 비용: {powerCost}";
        attackSpeedInfo.text = $"공격속도 (+0.1) / Lv.{p.AttackSpeedLevel} / 비용: {speedCost}";
        
        goldText.text = $"Gold: {p.Gold}";
    }
    
    
    public void UpdateGoldUI(int amount)
    {
        goldText.text = $"Gold: {amount}";
    }

    public void UpdateWaveUI(int wave)
    {
        waveText.text = $"Wave: {wave}";
    }
    
    public void UpdateStageUI(int stage)
    {
        maxStageText.text = $"스테이지: {stage}";
    }

    // public void OnAbilityPanelClose()
    // {
    //     abilityPanel.SetActive(false);
    //     UpdateUI();
    // }

    public void UpdatePlayerPanel()
    {
        // 능력치
        statText.text = $"이동속도: {Player.Instance.moveSpeed}\n" +
                        $"공격력: {Player.Instance.attackPower}\n" +
                        $"공격속도: {Player.Instance.attackCooldown}";

        // 인벤토리 갱신
        foreach (Transform t in inventoryGrid) Destroy(t.gameObject);
        foreach (var item in playerData.inventory)
        {
            Debug.Log($"슬롯 생성 중: {item.itemName}");
            var go = Instantiate(slotPrefab, inventoryGrid);
            go.GetComponent<ItemSlotUI>().Setup(item);
        }

        // 장착 아이템 갱신
        for (int i = 0; i < equipSlotImages.Length; i++)
        {
            if (i < playerData.equippedItems.Count && playerData.equippedItems[i] != null)
            {
                equipSlotImages[i].sprite = playerData.equippedItems[i].icon;
                equipSlotImages[i].enabled = true;
            }
            else
            {
                equipSlotImages[i].enabled = false; // 비워두기
            }
        }
        Debug.Log("업데이트");
    }

    // public void RefreshInventoryUI()
    // {
    //     for (int i = 0; i < inventorySlots.Length; i++)
    //     {
    //         if (i < PlayerInventory.Instance.items.Count)
    //         {
    //             inventorySlots[i].SetItem(PlayerInventory.Instance.items[i]);
    //         }
    //         else
    //         {
    //             inventorySlots[i].Clear();
    //         }
    //     }
    // }
    
    public void EquipItem(ItemData item)
    {
        int slotIndex = (int)item.equipSlot;
        Debug.Log($"[장착 시도] 아이템: {item.itemName}, 슬롯 인덱스: {slotIndex}");
        if (slotIndex < 0 || slotIndex >= playerData.equippedItems.Count)
        {
            Debug.LogWarning("장착 실패: 올바르지 않은 슬롯 인덱스");
            return;
        }

        // 기존 장비 인벤토리로
        var oldItem = playerData.equippedItems[slotIndex];
        if (oldItem != null)
        {
            playerData.inventory.Add(oldItem);
        }

        // 새 장비 장착
        playerData.inventory.Remove(item);
        playerData.equippedItems[slotIndex] = item;

        Player.Instance.RecalculateStats(playerData.equippedItems);
        UpdatePlayerPanel();
    }
    
    public void UnequipItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= playerData.equippedItems.Count) return;

        var equipped = playerData.equippedItems[slotIndex];
        if (equipped == null) return;

        playerData.inventory.Add(equipped);
        playerData.equippedItems[slotIndex] = null;

        Player.Instance.RecalculateStats(playerData.equippedItems);
        UpdatePlayerPanel();
    }
}
