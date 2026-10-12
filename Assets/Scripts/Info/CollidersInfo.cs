using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public struct CollidersInfo : IEquatable<CollidersInfo>
    {
        public bool hasSelectionCollider;
        public bool hasHarvestCollider;

        [NonSerialized] public GameObject SelectionColliderObject;
        [NonSerialized] public GameObject HarvestColliderObject;
        
        public CollidersInfo(bool hasSelectionCollider, bool hasHarvestCollider)
        {
            this.hasSelectionCollider = hasSelectionCollider;
            this.hasHarvestCollider = hasHarvestCollider;
            
            SelectionColliderObject = null;
            HarvestColliderObject = null;
        }

        public bool Equals(CollidersInfo other) => hasSelectionCollider == other.hasSelectionCollider &&
                                                   hasHarvestCollider == other.hasHarvestCollider;
        public override bool Equals(object obj) => obj is CollidersInfo other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(hasSelectionCollider, hasHarvestCollider);
        
        public static bool operator ==(CollidersInfo left, CollidersInfo right) => left.Equals(right);
        public static bool operator !=(CollidersInfo left, CollidersInfo right) => !left.Equals(right);
    }
}