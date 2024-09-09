using System;

public class Item
{
    public int id;
    public int count;
    public ItemGrade grade;

    public int remainingCount;
    public int upGradeCost;
    public int power;
    public int level;

    public void SetLevelAndPower()
    {
        var IOU = GM.I.csvReadeer.itemOptionUpgrade;
        power = 0;
        level = 0;
        var count = this.count;
        if (GM.I.csvReadeer.ItemDic.TryGetValue(id, out var item))
            power += item.defaultPower;
            
        for (int i = 0; i < IOU.Count; ++i)
        {
            var cost = i != IOU.Count - 1 ? IOU[i].upgradeBelowLimit * IOU[i].upgradeCost : int.MaxValue;
            if (count < cost)
            {
                var addLevel = count / IOU[i].upgradeCost;
                remainingCount = count % IOU[i].upgradeCost;
                upGradeCost = IOU[i].upgradeCost;
                power += addLevel * GetUpgradeValue(IOU[i]);
                level += addLevel;
                break;
            }
            power += IOU[i].upgradeBelowLimit * GetUpgradeValue(IOU[i]);
            level += IOU[i].upgradeBelowLimit;
            count -= IOU[i].upgradeBelowLimit * IOU[i].upgradeCost;
        }
    }

    private int GetUpgradeValue(ItemOptionUpgrade IOU)
    {
        return grade switch
        {
            ItemGrade.Normal => IOU.normalUpgradeValue,
            ItemGrade.Rare => IOU.rareUpgradeValue,
            ItemGrade.Epic => IOU.epicUpgradeValue,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static bool operator <(Item p1, Item p2) => p1.id < p2.id;
    public static bool operator >(Item p1, Item p2) => p1.id > p2.id;
    public static bool operator <=(Item p1, Item p2) => p1.id <= p2.id;
    public static bool operator >=(Item p1, Item p2) => p1.id >= p2.id;
}
