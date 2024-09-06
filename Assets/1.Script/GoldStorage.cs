using TMPro;
using UnityEngine;

public class GoldStorage : MonoBehaviour
{
    public float gold;
    public TMP_Text text;

    public void AddGold(float addGold)
    {
        gold += addGold;
        text.text = Utility.FormatNumber(gold);
    }
}
