using System;
using System.Collections.Generic;
using UnityEngine;

public static class DebugRegistry
{
    // Tuple of the label name (health, speed...) and a function that returns the current value
    public static readonly List<(string, Func<object>)> Fields = new();

    public static void Register(string name, Func<object> getter)
    {
        Fields.Add((name, getter));
    }
}
