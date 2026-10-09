using System.Globalization;
using UnityEngine;

namespace Selectica
{
    public static class Vector3Serializer
    {
        private const char SEPARATOR = ',';

        public static string ToEntry(Vector3 value)
        {
            var x = value.x.ToString(CultureInfo.InvariantCulture);
            var y = value.y.ToString(CultureInfo.InvariantCulture);
            var z = value.z.ToString(CultureInfo.InvariantCulture);
            return $"{x}{SEPARATOR}{y}{SEPARATOR}{z}";
        }

        public static Vector3 FromEntry(string entry)
        {
            var split = entry.Split(SEPARATOR);
            var x = float.Parse(split[0]);
            var y = float.Parse(split[1]);
            var z = float.Parse(split[2]);
            return new Vector3(x, y, z);
        }
    }
}