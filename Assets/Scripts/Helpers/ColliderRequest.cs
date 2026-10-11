using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public struct ColliderRequest : IEquatable<ColliderRequest>
    {
        public ColliderType colliderType;
        public Action<GameObject> Callback;
        
        public bool Equals(ColliderRequest other) => colliderType == other.colliderType;
        public override bool Equals(object obj) => obj is ColliderRequest other && Equals(other);
        
        public override int GetHashCode() => colliderType.GetHashCode();
        
        public static bool operator ==(ColliderRequest left, ColliderRequest right) => left.Equals(right);
        public static bool operator !=(ColliderRequest left, ColliderRequest right) => !left.Equals(right);
    }
}