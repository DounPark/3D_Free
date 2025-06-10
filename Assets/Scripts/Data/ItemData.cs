using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public ItemType itemType;
    public int value; // 예: 공격력+2, 이동속도+1 등
}

public enum ItemType { Weapon, Armor, Accessory, Consumable }
