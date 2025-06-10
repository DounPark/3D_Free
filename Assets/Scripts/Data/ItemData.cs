using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    [TextArea] public string description;
    public Sprite icon;
    public ItemType itemType;

    [Header("장비 관련")]
    public EquipSlot equipSlot;
    public int movementSpeed;
    public int attackPower;
    public float attackSpeed;

    [Header("소비/재료 관련")]
    public int moveSpeedUp;
    public int attackPowerUp;
    public int itemValue; // 상점 가격 등

    [Header("기타")]
    public GameObject itemPrefab; // 드롭용 프리팹 등
}

public enum ItemType { Equipment, Consumable, Material, Quest }
public enum EquipSlot { None, Head, Body, Weapon, Accessory }
