using UnityEngine;
using UnityEngine.UI;

public class EquipmentTab : MonoBehaviour
{
    public Gacha gacha;
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
            if (isOn)
                gacha.activeTab = ItemType.Weapon;
            gacha.inventory.SetInventory(ItemType.Weapon);
            gacha.gachaKey = 10000;
        });
        armorToggle.onValueChanged.AddListener((isOn) =>
        {
            ChangeBGColor(armorBG, isOn);
            if (isOn)
                gacha.activeTab = ItemType.Armor;
            gacha.inventory.SetInventory(ItemType.Armor);
            gacha.gachaKey = 20000;
        });
        shieldToggle.onValueChanged.AddListener((isOn) =>
        {
            ChangeBGColor(shieldBG, isOn);
            if (isOn)
                gacha.activeTab = ItemType.Shield;
            gacha.inventory.SetInventory(ItemType.Shield);
            gacha.gachaKey = 30000;
        });
        
        ChangeBGColor(armorBG, false);
        ChangeBGColor(shieldBG, false);
    }
    private void ChangeBGColor(Image img, bool isOn)
    {
        img.color = isOn ? ColorPalette.ultramarine : ColorPalette.ultramarine + new Color32(60, 60, 60, 255);
    }
}
