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
        if (itemSlotGroups.Count < (keyList.Count / 4) + 1)
        {
            var count = (keyList.Count / 4) + 1 - itemSlotGroups.Count;
            for (int i = 0; i < count; ++i)
            {
                var rect = (RectTransform)itemSlotGroupPrefab.transform;
                itemSlotGroups.Add(Instantiate(
                    itemSlotGroupPrefab,
                    new Vector3(0, rect.rect.height * itemSlotGroups.Count + 50, 0),
                    Quaternion.identity,
                    scrollContent.transform));
            }
        }

        for (int i = 1; i < keyList.Count + 1; ++i)
        {
            var key = keyList[i - 1];
            itemSlotGroups[i / 4].SetSlot(GM.I.player.inventory[type][key], i % 4);
        }
    }
}
