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
    public Dictionary<(ItemGrade, GachaRewardItemType), (int, int)> GachaRandomBag;
    [ReadOnly] public SerializableDictionary<int, ItemInfo> ItemDic;

    private void Awake()
    {
        ItemDic.Build();
    }
}

[Serializable]
public class SerializableDictionary<TKey ,TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    [Serializable]
    public struct Pair
    {
        public TKey key;
        public TValue value;
    }

    [SerializeField] private List<Pair> Dictionary;
    private Dictionary<TKey, TValue> dic = new();
    public void Build()
    {
        foreach (var pair in Dictionary)
        {
            dic.TryAdd(pair.key, pair.value);
        }
    }


    public void Clear()
    {
        dic.Clear();
        Dictionary.Clear();
    }
    public void Add(TKey key, TValue value)
    {
        if (Application.isPlaying)
            return;

        Dictionary.Add(new Pair()
        {
            key = key,
            value = value
        });
    }

    public bool ContainsKey(TKey key) => dic.ContainsKey(key);
    public bool TryGetValue(TKey key, out TValue value)
    {
        value = dic.TryGetValue(key ,out var newVal) ? newVal : default;
        return dic.ContainsKey(key);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return dic.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return dic.GetEnumerator();
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(CSVReadeer))]
public class CSVReadeerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 표시
        DrawDefaultInspector();

        // 대상 스크립트를 가져옵니다.
        var myScript = (CSVReadeer)target;
        // 버튼 추가
        if (GUILayout.Button("Set Item List"))
        {
            myScript.ItemDic.Clear();

            // myScript.ItemList = new StringIntPair<int, ItemInfo>();
            string csvFilePath = "Assets/2.Data/ItemList.csv";
            string[] lines = File.ReadAllLines(csvFilePath);
            for (var i = 2; i < lines.Length; ++i)
            {
                var output = lines[i];
                var str = output.Split(',').ToArray();
                myScript.ItemDic.Add(int.Parse(str[0]), new ItemInfo()
                {
                    grade = Enum.Parse<ItemGrade>(str[1]),
                    type = Enum.Parse<ItemOptionType>(str[2]),
                    defaultValue = int.Parse(str[3]),
                    IconPath = str[4],
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
public enum GachaRewardItemType
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