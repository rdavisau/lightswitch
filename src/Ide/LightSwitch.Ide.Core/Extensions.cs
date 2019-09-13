using System;
using System.Collections.Generic;

namespace LightSwitch.Ide.Core
{
    public static class Extensions
    {
        public static void Deconstruct<TKey, TValue>(
            this KeyValuePair<TKey, TValue> kvp,
            out TKey key, out TValue value)
        {
            key = kvp.Key;
            value = kvp.Value;
        }
    }
}
