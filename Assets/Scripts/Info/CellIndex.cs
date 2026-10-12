using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public struct CellIndex : IEquatable<CellIndex>
    {
        public int x;
        public int z;
        [HideInInspector] public bool valid;
        
        public CellIndex(int x, int z)
        { this.x = x; this.z = z; valid = true; }
        
        public bool Equals(CellIndex other) => x == other.x && z == other.z && valid == other.valid;
        public override bool Equals(object obj) => obj is CellIndex other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(x, z, valid);
        
        public static bool operator ==(CellIndex left, CellIndex right) => left.Equals(right);
        public static bool operator !=(CellIndex left, CellIndex right) => !left.Equals(right);
        
        private const char SEPARATOR = ',';
        public override string ToString() => $"({x}{SEPARATOR}{z})";
        public static CellIndex Parse(string s)
        { var split = s.Split(SEPARATOR); return new CellIndex(int.Parse(split[0]), int.Parse(split[1])); }
    }
}