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
    public Transform inventoryGrid;
    public Transform equipGrid;
    
    public Button closePlayerButton;

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
    
    void UpdatePlayerPanel()
    {
        // 능력치
        statText.text = $"이동속도: {Player.Instance.moveSpeed}\n" +
                        $"공격력: {Player.Instance.attackPower}\n" +
                        $"공격속도: {Player.Instance.attackCooldown}";

        // 인벤토리
        foreach (Transform t in inventoryGrid) Destroy(t.gameObject);
        foreach (var item in playerData.inventory)
        {
            // 아이템 슬롯 프리팹 생성 후 아이콘/이름 등 표시
            // 예시: Instantiate(slotPrefab, inventoryGrid).GetComponent<ItemSlotUI>().Setup(item);
        }

        // 장착 아이템
        foreach (Transform t in equipGrid) Destroy(t.gameObject);
        foreach (var item in playerData.equippedItems)
        {
            // 장착 슬롯 프리팹 생성
        }
    }
}
