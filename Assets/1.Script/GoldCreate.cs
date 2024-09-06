using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GoldCreate : MonoBehaviour
{
    public GoldStorage goldStorage;
    public TMP_Text createText;
    public TMP_Text timerText;
    public float goldCreateTime = 1f;
    public float goldAmount = 0.75f;
    private float time = 0f;
    private double lastEnable;
    private void OnEnable()
    {
        var disableTime = Time.unscaledTime - lastEnable;
        if (disableTime == 0)
            return;
        Foo((int)(disableTime / time));
    }

    private void Start()
    {
        StartCoroutine(CreateTextChange());
    }

    private void OnDisable()
    {
        lastEnable = Time.unscaledTime;
    }

    private void Update()
    {
        time += Time.deltaTime;
        while(goldCreateTime <= time)
        {
            time -= goldCreateTime;
            goldStorage.AddGold(goldAmount);
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
        goldStorage.AddGold(goldAmount * gold);
    }
}
