using TMPro;
using UnityEngine;

public class GoldStorage : MonoBehaviour
{
    public float gold;
    public TMP_Text text;

    public void AddGold(float addGold)
    {
        gold += addGold;
        Debug.Log(gold);
        text.text = $"{gold:F1}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
