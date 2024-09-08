using System;
using UnityEngine;

public static class ColorPalette
{
    public static Color ultramarine = new Color32(22, 96, 131, 255);
    public static Color font = new Color32(50, 50, 50, 255);
    private static readonly Color normalGrade;
    private static readonly Color rareGrade;
    private static readonly Color epicGrade;
    
    static ColorPalette()
    {
        if (ColorUtility.TryParseHtmlString("#9DA8B6", out var normalColor))
            normalGrade = normalColor;
        if (ColorUtility.TryParseHtmlString("#9DA8B6", out var rareColor))
            rareGrade = rareColor;
        if (ColorUtility.TryParseHtmlString("#9DA8B6", out var epicColor))
            epicGrade = epicColor;
    }

    public static Color GetGradeColor(ItemGrade grade)
    {
        return grade switch
        {
            ItemGrade.Normal => normalGrade,
            ItemGrade.Rare => rareGrade,
            ItemGrade.Epic => epicGrade,
            _ => throw new ArgumentOutOfRangeException(nameof(grade), grade, null)
        };
    }
}
