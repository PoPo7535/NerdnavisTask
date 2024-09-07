using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    private const int GachaValue = 100;
    public Button gacha1Btn;
    public Button gacha10Btn;
    public Button gacha100Btn;

    private void Start()
    {
        gacha1Btn.onClick.AddListener(() =>
        {
            GameManager.I.player.gold -= GachaValue;
            SetActiveBtn();
        });
        gacha10Btn.onClick.AddListener(() =>
        {
            GameManager.I.player.gold -= GachaValue * 10; 
            SetActiveBtn();
        });
        gacha100Btn.onClick.AddListener(() =>
        {
            GameManager.I.player.gold -= GachaValue * 100; 
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
        gacha1Btn.interactable = GameManager.I.player.gold >= GachaValue;
        gacha10Btn.interactable = GameManager.I.player.gold >= GachaValue * 10;
        gacha100Btn.interactable = GameManager.I.player.gold >= GachaValue * 100; 
    }
}
