using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    private ItemData item;

    public void Setup(ItemData newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        nameText.text = item.itemName;
    }

    public void OnClick()
    {
        if (item == null) return;

        if (item.itemType == ItemType.Equipment)
        {
            UIManager.Instance.EquipItem(item); // 👉 UIManager에서 장착 처리
        }
    }
}
