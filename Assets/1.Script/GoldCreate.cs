using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class GoldCreate : MonoBehaviour
{
    public GoldStorage goldStorage;
    public TMP_Text createText;
    public TMP_Text timerText;
    private float refillMoneyInterval = 1f;
    private float refillMoneyCount = 0.75f;
    private float time = 0f;
    private double lastEnable;
    private Coroutine cor;
    private void OnEnable()
    {
        cor = StartCoroutine(CreateTextChange());
        var disableTime = Time.unscaledTime - lastEnable;
        if (disableTime != 0)
        {
            Foo((int)(disableTime / time));
        }
    }

    private void Start()
    {
        refillMoneyInterval = GM.I.refillMoneyInterval;
        refillMoneyCount = GM.I.refillMoneyCount;
    }

    private void OnDisable()
    {
        lastEnable = Time.unscaledTime;
        StopCoroutine(cor);
    }

    private void Update()
    {
        time += Time.deltaTime;
        while(refillMoneyInterval <= time)
        {
            time -= refillMoneyInterval;
            goldStorage.AddGold(refillMoneyCount);
        }
        timerText.text = $"{time:F1}s";
    }

    private IEnumerator CreateTextChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            createText.text = "제작중 . ";
            yield return new WaitForSeconds(0.4f);
            createText.text = "제작중 . . ";
            yield return new WaitForSeconds(0.4f);
            createText.text = "제작중 . . .";
        }
    }
    private void Foo(float gold)
    {
        goldStorage.AddGold(refillMoneyCount * gold);
    }
}
