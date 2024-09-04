using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CSVReadeer : MonoBehaviour
{
    public SerializableDic<int, ItemInfo> ItemDic;
    public SerializableDicInList<GachaKey, GachaValue> gachaBag;
    private void Awake()
    {
        ItemDic.Build();
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
        if (GUILayout.Button("Set Item List"))
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
                    defaultValue = int.Parse(str[3]),
                    IconPath = $"3.{str[4]}",
                });
            }
        }
        
        if (GUILayout.Button("Set Gacha Bag"))
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
    }
}
#endif


[Serializable]
public struct ItemInfo
{
    public ItemGrade grade;
    public ItemOptionType type;
    public int defaultValue;
    public string IconPath;
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