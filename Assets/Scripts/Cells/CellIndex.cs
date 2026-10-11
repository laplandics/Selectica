using System;

namespace Selectica
{
    [Serializable]
    public struct CellIndex : IEquatable<CellIndex>
    {
        public int x;
        public int y;

        public CellIndex(int x, int y)
        { this.x = x; this.y = y; }
        
        public bool Equals(CellIndex other) => x == other.x && y == other.y;
        public override bool Equals(object obj) => obj is CellIndex other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(x, y);
        
        public static bool operator ==(CellIndex left, CellIndex right) => left.Equals(right);
        public static bool operator !=(CellIndex left, CellIndex right) => !left.Equals(right);
        
        private const char SEPARATOR = ',';
        public override string ToString() => $"({x}, {y})";
        public static CellIndex Parse(string s)
        { var split = s.Split(SEPARATOR); return new CellIndex(int.Parse(split[0]), int.Parse(split[1])); }
    }
}