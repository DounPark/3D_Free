using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotUI : MonoBehaviour
{
    public int slotIndex;
    
    public void OnClick()
    {
        UIManager.Instance.UnequipItem(slotIndex);
    }
}
