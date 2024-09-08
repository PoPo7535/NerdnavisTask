using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CSVReadeer : MonoBehaviour
{
    public SerializableDic<int, ItemInfo> ItemDic;
    public SerializableDicInList<GachaKey, GachaValue> gachaBag;
    public SerializableDic<int, GachaRate> gachaGradeInfo;
    public List<ItemOptionUpgrade> itemOptionUpgrade;
    private void Awake()
    {
        ItemDic.Build();
        gachaBag.Build();
        gachaGradeInfo.Build();
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(CSVReadeer))]
public class CSVReadeerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var myScript = (CSVReadeer)target;
        if (GUILayout.Button("Set ItemList"))
        {
            myScript.ItemDic.Clear();

            var csvFilePath = "Assets/2.Data/ItemList.csv";
            var lines = File.ReadAllLines(csvFilePath);
            for (var i = 2; i < lines.Length; ++i)
            {
                var output = lines[i];
                var str = output.Split(',').ToArray();
                myScript.ItemDic.Add(int.Parse(str[0]), new ItemInfo()
                {
                    grade = Enum.Parse<ItemGrade>(str[1]),
                    type = Enum.Parse<ItemOptionType>(str[2]),
                    defaultPower = int.Parse(str[3]),
                    icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/3.{str[4]}.png"),
                });
            }
        }
        
        if (GUILayout.Button("Set GachaBag"))
        {
            myScript.gachaBag.Clear();

            var csvFilePath = "Assets/2.Data/GachaRandomBag.csv";
            var lines = File.ReadAllLines(csvFilePath);
            for (var i = 2; i < lines.Length; ++i)
            {
                var output = lines[i];
                var str = output.Split(',').ToArray();
                myScript.gachaBag.Add(
                    new GachaKey() 
                    {
                        dropID = int.Parse(str[0]),
                        grade = Enum.Parse<ItemGrade>(str[1]),
                    }, 
                    new GachaValue() 
                    {
                        type = Enum.Parse<ItemType>(str[2]),
                        itemID = int.Parse(str[3]),
                    });
            }
        }
        
        if (GUILayout.Button("Set GachaGradeInfo"))
        {
            myScript.gachaGradeInfo.Clear();

            var csvFilePath = "Assets/2.Data/GachaGradeInfo.csv";
            var lines = File.ReadAllLines(csvFilePath);
            for (var i = 2; i < lines.Length; ++i)
            {
                var output = lines[i];
                var str = output.Split(',').ToArray();
                myScript.gachaGradeInfo.Add(int.Parse(str[0]),
                    new GachaRate()
                    {
                        normalRate = int.Parse(str[1]),
                        rareRate = int.Parse(str[2]),
                        epicRate = int.Parse(str[3]),
                        gachaRandombagID = int.Parse(str[4]),
                    });
            }
        }
        
        if (GUILayout.Button("Set ItemOptionUpgrade"))
        {
            myScript.itemOptionUpgrade.Clear();

            var csvFilePath = "Assets/2.Data/ItemOptionUpgrade.csv";
            var lines = File.ReadAllLines(csvFilePath);
            for (var i = 2; i < lines.Length; ++i)
            {
                var output = lines[i];
                var str = output.Split(',').ToArray();
                myScript.itemOptionUpgrade.Add(
                    new ItemOptionUpgrade()
                    {
                        upgradeBelowLimit = int.Parse(str[0]),
                        upgradeCost = int.Parse(str[1]),
                        normalUpgradeValue = int.Parse(str[2]),
                        rareUpgradeValue = int.Parse(str[3]),
                        epicUpgradeValue = int.Parse(str[4]),
                    });
            }
        }
        
        if (GUILayout.Button("Set GlobalValue"))
        {
            var csvFilePath = "Assets/2.Data/GlobalValue.csv";
            var lines = File.ReadAllLines(csvFilePath);
            var gameManager = myScript.GetComponent<GM>();
            gameManager.refillMoneyInterval = int.Parse(lines[2].Split(',')[1]);
            gameManager.refillMoneyCount = float.Parse(lines[3].Split(',')[1]);
            gameManager.defaultMoneyCount = int.Parse(lines[4].Split(',')[1]);
            gameManager.requireGachaPrice = int.Parse(lines[5].Split(',')[1]);
            gameManager.maxMoneyLimit = int.Parse(lines[6].Split(',')[1]);
        }
    }
}
#endif


[Serializable]
public struct ItemInfo
{
    public ItemGrade grade;
    public ItemOptionType type;
    public int defaultPower;
    public Sprite icon;
}
[Serializable]
public struct GachaKey
{
    public int dropID;
    public ItemGrade grade;
}
[Serializable]
public struct GachaValue
{
    public ItemType type;
    public int itemID;
}
[Serializable]
public struct ItemOptionUpgrade
{
    public int upgradeBelowLimit;
    public int upgradeCost;
    public int normalUpgradeValue;
    public int rareUpgradeValue;
    public int epicUpgradeValue;
}
[Serializable]
public struct GachaRate
{
    public int normalRate;
    public int rareRate;
    public int epicRate;
    public int gachaRandombagID;

    public ItemGrade GetRandomItemGrade()
    {
        var val = Random.Range(1, 100);
        if (val <= normalRate)
            return ItemGrade.Normal;
        if (val <= normalRate + rareRate)
            return ItemGrade.Rare;
        return ItemGrade.Epic;
    }
}
public enum ItemType
{
    Weapon,
    Armor,
    Shield
}
public enum ItemGrade
{
    Normal,
    Rare,
    Epic
}
public enum ItemOptionType
{
    AttackIncrease,
    DefenseIncrease,
    HpIncrease
}

