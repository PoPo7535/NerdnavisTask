using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldStorage : MonoBehaviour
{
    public TMP_Text text;
    public Button goldGetBtn;
    public GameObject arrowBox;
    
    private float gold;

    public void Start()
    {
        ActiveGetBtn();
        goldGetBtn.onClick.AddListener(GetGold);
    }

    public void AddGold(float addGold)
    {
        gold += addGold;
        text.text = Utility.FormatNumber(gold);
        ActiveGetBtn();
    }

    private void GetGold()
    {
        GM.I.player.gold += (int)gold;
        gold = 0;
        text.text = Utility.FormatNumber(gold);
        ActiveGetBtn();
    }
    
    private void ActiveGetBtn()
    {
        var active = 100 <= gold;
        goldGetBtn.interactable = active;
        arrowBox.SetActive(active);
    }

#if UNITY_EDITOR

    private GUIStyle guiStyle = new();
    private void OnGUI()
    {
        guiStyle = GUI.skin.button;
        guiStyle.fontSize = 30;
        guiStyle.normal.textColor = Color.green;
        if (GUI.Button(new Rect(50, 330, 200, 70), "AddGold", guiStyle)) 
        {
            AddGold(500);
        };
    }


#endif
}
