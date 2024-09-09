using System;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Button detailBtn;
    public TMP_Text countText;
    public TMP_Text levelText;
    public Image icon;
    public Image gradeColorImg;
    public Slider slider;
    
    public void SetItemSlot(Item item)
    {
        // icon.sprite = iconSpr;
        // gradeColorImg.color = gradeColor;
        // levelText.text = $"Lv.{level}";
        // icon.sprite = item.;
        detailBtn?.onClick.RemoveAllListeners();
        detailBtn?.onClick.AddListener(() =>
        {
            GM.I.itemSlotPopUp.ShowPopUp(item);
        });
        if (GM.I.csvReadeer.ItemDic.TryGetValue(item.id, out var itemInfo))
        {
            
            icon.sprite = itemInfo.icon;
            
            gradeColorImg.color = ColorPalette.GetGradeColor(itemInfo.grade);
            levelText.text = $"Lv.{item.level}";
            countText.text = $"{item.remainingCount}/{item.upGradeCost}";
            slider.value = (float)item.remainingCount / item.upGradeCost;
        }
    }
    
}
