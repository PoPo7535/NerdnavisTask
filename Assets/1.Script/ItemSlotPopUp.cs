using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotPopUp : MonoBehaviour
{
    public Button closeBtn;
    public ItemSlot itemSlot;
    public TMP_Text powerText;

    public Image stateImg;
    public Sprite attackImg;
    public Sprite defenseImg;
    public Sprite hpImg;
    public void Start()
    {
        closeBtn.onClick.AddListener(ClosePopUp);
    }

    public void ShowPopUp(Item item)
    {
        gameObject.SetActive(true);
        itemSlot.SetItemSlot(item);
        powerText.text = item.power.ToString();
        stateImg.sprite = item.type switch
        {
            ItemType.Weapon => attackImg,
            ItemType.Armor => defenseImg,
            ItemType.Shield => hpImg,
            _ => attackImg
        };
    }

    private void ClosePopUp()
    {
        gameObject.SetActive(false);
    }
}
