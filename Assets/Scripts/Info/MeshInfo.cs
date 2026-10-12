using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public struct MeshInfo : IEquatable<MeshInfo>
    {
        public string meshName;
        public string materialName;

        [NonSerialized] public GameObject MeshObject;
        
        public MeshInfo(string meshName, string materialName)
        {
            this.meshName = meshName;
            this.materialName = materialName;
            MeshObject = null;
        }
        
        public bool Equals(MeshInfo other) => meshName == other.meshName && materialName == other.materialName;
        public override bool Equals(object obj) => obj is MeshInfo other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(meshName, materialName);
        
        public static bool operator ==(MeshInfo left, MeshInfo right) => left.Equals(right);
        public static bool operator !=(MeshInfo left, MeshInfo right) => !left.Equals(right);
    }
}