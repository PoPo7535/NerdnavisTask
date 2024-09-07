using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    public Button gacha1Btn;
    public Button gacha10Btn;
    public Button gacha100Btn;

    private void Start()
    {
        gacha1Btn.onClick.AddListener(() =>
        {
            GM.I.player.gold -= GM.I.requireGachaPrice;
            if (GM.I.csvReadeer.gachaGradeInfo.TryGetValue(10000, out var gachaRate))
            {
                var grade = gachaRate.GetRandomItemGrade();
                if (GM.I.csvReadeer.gachaBag.TryGetValue(new GachaKey()
                    {
                        grade = grade,
                        dropID = gachaRate.gachaRandombagID
                    }, out var gachaValues))
                {
                    var asd = GM.I.player.inventory[gachaValues[0].type];
                };
            }
            SetActiveBtn();
        });
        gacha10Btn.onClick.AddListener(() =>
        {
            GM.I.player.gold -= GM.I.requireGachaPrice * 10; 
            SetActiveBtn();
        });
        gacha100Btn.onClick.AddListener(() =>
        {
            GM.I.player.gold -= GM.I.requireGachaPrice * 100; 
            SetActiveBtn();
        });
    }

    public void Foo()
    {
        // GameManager.I.csvReadeer.gachaBag
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
