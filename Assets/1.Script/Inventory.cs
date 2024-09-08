using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemSlotGroup itemSlotGroupPrefab;
    public GameObject scrollContent;
    private readonly List<ItemSlotGroup> itemSlotGroups = new();
    public void SetInventory(ItemType type)
    {
        foreach (var itemSlotGroup in itemSlotGroups)
        {
            itemSlotGroup.SetDisableSlot();
            
        }
        var dic = GM.I.player.inventory[type];
        var keyList = dic.Keys.ToList();
        keyList.Sort();
        var itemSlotGroupRect = (RectTransform)itemSlotGroupPrefab.transform;
        if (itemSlotGroups.Count < (keyList.Count / 4) + 1)
        {
            var count = (keyList.Count / 4) + 1 - itemSlotGroups.Count;
            for (int i = 0; i < count; ++i)
            {
                var itemSlotGroup = Instantiate(
                    itemSlotGroupPrefab,
                    Vector3.zero,
                    Quaternion.identity,
                    scrollContent.transform);
                itemSlotGroups.Add(itemSlotGroup);
                var slotRect = (RectTransform)itemSlotGroup.transform;
                slotRect.anchorMin = new Vector2(0, 1);
                slotRect.anchoredPosition = new Vector2(0,
                    -(itemSlotGroupRect.rect.height / 2 + 
                      itemSlotGroupRect.rect.height * (itemSlotGroups.Count - 1)));
            }
        }

        // var scrollRect = (RectTransform)scrollContent.transform;
        // scrollRect.sizeDelta.Set(scrollRect.rect.width,
        //     itemSlotGroupRect.rect.height * (itemSlotGroups.Count + 1));
             
        for (int i = 0; i < keyList.Count; ++i)
        {
            var key = keyList[i];
            itemSlotGroups[i / 4].SetSlot(GM.I.player.inventory[type][key], i % 4);
        }
    }
}
