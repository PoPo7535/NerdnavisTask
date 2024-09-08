using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Gacha : MonoBehaviour
{
    public Button gacha1Btn;
    public Button gacha10Btn;
    public Button gacha100Btn;

    private int gachaID = 10000;
    private void Start()
    {
        gacha1Btn.onClick.AddListener(() =>
        {
            GM.I.player.gold -= GM.I.requireGachaPrice;
            if (GM.I.csvReadeer.gachaGradeInfo.TryGetValue(gachaID, out var gachaRate))
                ExecuteGacha(gachaRate, 1);
            SetActiveBtn();
        });
        gacha10Btn.onClick.AddListener(() =>
        {
            GM.I.player.gold -= GM.I.requireGachaPrice * 10; 
            if (GM.I.csvReadeer.gachaGradeInfo.TryGetValue(gachaID, out var gachaRate))
                ExecuteGacha(gachaRate, 10);
            SetActiveBtn();
        });
        gacha100Btn.onClick.AddListener(() =>
        {
            GM.I.player.gold -= GM.I.requireGachaPrice * 100; 
            if (GM.I.csvReadeer.gachaGradeInfo.TryGetValue(gachaID, out var gachaRate))
                ExecuteGacha(gachaRate, 100);
            SetActiveBtn();
        });
    }

    private void ExecuteGacha(GachaRate gachaRate, int count)
    {
        var hash = new HashSet<int>();
        Dictionary<int, Item> dic = new();
        for (int i = 0; i < count; ++i)
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
