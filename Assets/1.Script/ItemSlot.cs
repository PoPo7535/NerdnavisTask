using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public TMP_Text countText;
    public TMP_Text levelText;
    public Image icon;
    public Image gradeColorImg;
    public Slider slider;

    public void SetItemSlot(Sprite iconSpr, Color gradeColor, int level, int count)
    {
        icon.sprite = iconSpr;
        gradeColorImg.color = gradeColor;
        levelText.text = $"Lv.{level}";
    }
}
