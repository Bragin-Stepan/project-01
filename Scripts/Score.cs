using System.Collections;
using UnityEngine;

public static class Score
{
    public static int Value = 0;
    public static void SetValue(int value) => Value = value;
    public static void Reset() => Value = 0;
}
