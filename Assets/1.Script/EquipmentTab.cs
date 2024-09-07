using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentTab : MonoBehaviour
{
    public Toggle weaponToggle;
    public Image weaponBG;
    public Toggle armorToggle;
    public Image armorBG;
    public Toggle shieldToggle;
    public Image shieldBG;

    void Start()
    {
        weaponToggle.onValueChanged.AddListener((isOn) =>
        {
            ChangeBGColor(weaponBG, isOn);
        });
        armorToggle.onValueChanged.AddListener((isOn) =>
        {
            ChangeBGColor(armorBG, isOn);
        });
        shieldToggle.onValueChanged.AddListener((isOn) =>
        {
            ChangeBGColor(shieldBG, isOn);
        });
        
        ChangeBGColor(armorBG, false);
        ChangeBGColor(shieldBG, false);
    }
    private void ChangeBGColor(Image img, bool isOn)
    {
        img.color = isOn ? ColorPalette.ultramarine : ColorPalette.ultramarine + new Color32(60, 60, 60, 255);
    }
}
