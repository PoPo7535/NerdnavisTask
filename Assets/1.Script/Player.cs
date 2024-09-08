using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Dictionary<ItemType, Dictionary<int, Item>> inventory = new();
    
    public TMP_Text battlePowerText;
    public TMP_Text hpText;
    public TMP_Text defenseText;
    public TMP_Text attackText;
    public TMP_Text goldText;
    
    private int BattlePower => defense + attack + hp;
    private int hp;
    private int defense;
    private int attack;
    private int _gold;

    public int gold
    {
        get => _gold;
        set
        {
            _gold = value;
            goldText.text = Utility.FormatNumber(_gold);
        }
    }


    private void Start()
    {
        gold = GM.I.defaultMoneyCount;
        
        battlePowerText.text = Utility.FormatNumber(BattlePower);
        goldText.text = Utility.FormatNumber(gold);
        attackText.text = Utility.FormatNumber(attack);
        defenseText.text = Utility.FormatNumber(defense);
        hpText.text = Utility.FormatNumber(hp);

        inventory.Add(ItemType.Armor,  new Dictionary<int, Item>());
        inventory.Add(ItemType.Shield, new Dictionary<int, Item>());
        inventory.Add(ItemType.Weapon, new Dictionary<int, Item>());
    }

    public void AddAttack(int addAttack)
    {
        attack += addAttack;
        attackText.text = Utility.FormatNumber(attack);
        battlePowerText.text = Utility.FormatNumber(BattlePower);
    }
    public void AddDefense(int addDefense)
    {
        defense += addDefense;
        defenseText.text = Utility.FormatNumber(defense);
        battlePowerText.text = Utility.FormatNumber(BattlePower);
    }
    public void AddHp(int addHp)
    {
        hp += addHp;
        hpText.text = Utility.FormatNumber(hp);
        battlePowerText.text = Utility.FormatNumber(BattlePower);
    }
}
