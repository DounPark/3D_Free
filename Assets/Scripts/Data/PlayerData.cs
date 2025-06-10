using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed = 3f;
    public float attackPower = 5f;
    public float attackSpeed = 1f;

    // 인벤토리와 장착 아이템 (아이템 ScriptableObject 참조)
    public List<ItemData> inventory = new List<ItemData>();
    public List<ItemData> equippedItems = new List<ItemData>();
}
