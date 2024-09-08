using UnityEngine;

public class ItemSlotGroup : MonoBehaviour
{
    public ItemSlot[] slots;

    public void SetDisableSlot()
    {
        foreach (var slot in slots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void SetSlot(Item item, int slotNum)
    {
        slots[slotNum].gameObject.SetActive(true);
        slots[slotNum].SetItemSlot(item);
    }
}
