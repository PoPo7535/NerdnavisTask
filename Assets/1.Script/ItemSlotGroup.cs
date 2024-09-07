using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotGroup : MonoBehaviour
{
    public ItemSlot[] slots;

    public void SetSlotActive()
    {
        foreach (var slot in slots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void SetSlot(int slotNum)
    {
        slots[slotNum].gameObject.SetActive(true);
    }

}
