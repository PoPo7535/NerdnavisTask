using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class Utility 
{
    public static string FormatNumber(double num)
    {
        return num switch
        {
            >= 1_000_000_000 => (num / 1_000_000_000).ToString("0.##") + "G",
            >= 1_000_000 => (num / 1_000_000).ToString("0.##") + "M",
            >= 1_000 => (num / 1_000).ToString("0.##") + "K",
            _ => num.ToString()
        };
    }
}
