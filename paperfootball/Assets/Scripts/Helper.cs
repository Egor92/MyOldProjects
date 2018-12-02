using UnityEngine;
using System;

public static class Helper
{
    public static bool HasFlag(this FieldState value, FieldState flag)
    {
        return ((int)value & (int)flag) == (int)flag;
    }
}