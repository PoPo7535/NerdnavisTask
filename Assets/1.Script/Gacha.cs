using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Gacha : MonoBehaviour
{
    public Button gacha1Btn;
    public Button gacha10Btn;
    public Button gacha100Btn;
    public Inventory inventory;
    public int gachaKey = 10000;
    public ItemType activeTab = ItemType.Weapon;
    private void Start()
    {
        gacha1Btn.onClick.AddListener(() =>
        {
            GM.I.player.gold -= GM.I.requireGachaPrice;
            if (GM.I.csvReadeer.gachaGradeInfo.TryGetValue(gachaKey, out var gachaRate))
                ExecuteGacha(gachaRate, 1);
            inventory.SetInventory(activeTab);
            SetActiveBtn();
        });
        gacha10Btn.onClick.AddListener(() =>
        {
            GM.I.player.gold -= GM.I.requireGachaPrice * 10; 
            if (GM.I.csvReadeer.gachaGradeInfo.TryGetValue(gachaKey, out var gachaRate))
                ExecuteGacha(gachaRate, 10);
            inventory.SetInventory(activeTab);
            SetActiveBtn();
        });
        gacha100Btn.onClick.AddListener(() =>
        {
            GM.I.player.gold -= GM.I.requireGachaPrice * 100; 
            if (GM.I.csvReadeer.gachaGradeInfo.TryGetValue(gachaKey, out var gachaRate))
                ExecuteGacha(gachaRate, 100);
            inventory.SetInventory(activeTab);
            SetActiveBtn();
        });
    }

    private void ExecuteGacha(GachaRate gachaRate, int gachaCount)
    {
        var hash = new HashSet<int>();
        Dictionary<int, Item> dic = new();
        for (int i = 0; i < gachaCount; ++i)
        {
            var grade = gachaRate.GetRandomItemGrade();
            if (GM.I.csvReadeer.gachaBag.TryGetValue(new GachaKey()
                {
                    grade = grade,
                    dropID = gachaRate.gachaRandombagID
                }, out var gachaValues))
            {
                var randomVal = Random.Range(0, gachaValues.Count - 1);
                dic = GM.I.player.inventory[gachaValues[randomVal].type];
                var itemID = gachaValues[randomVal].itemID;
                if (dic.TryGetValue(itemID, out var item))
                {
                    ++item.count;
                }
                else
                {
                    hash.Add(itemID);
                    dic.Add(itemID, new Item()
                    {
                        id = itemID,
                        count = 1,
                        grade = grade
                    });
                }
                dic[itemID].SetLevelAndPower();
            }
        }

        foreach (var itemID in hash)
        {
            dic[itemID].SetLevelAndPower();
        }
    }

    private void OnEnable()
    {
        SetActiveBtn();
    }

    private void SetActiveBtn()
    {
        gacha1Btn.interactable = GM.I.player.gold >= GM.I.requireGachaPrice;
        gacha10Btn.interactable = GM.I.player.gold >= GM.I.requireGachaPrice * 10;
        gacha100Btn.interactable = GM.I.player.gold >= GM.I.requireGachaPrice * 100; 
    }
}
