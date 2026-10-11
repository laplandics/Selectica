using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class MeshComponent : EntityComponent_d
    {
        private string _materialName;
        private string _meshName;

        public MeshComponent(ComponentData data) : base(data)
        { _materialName = data.values[0]; _meshName = data.values[1]; }

        protected override void OnBind()
        {
            var visualObject = new GameObject("Visual");
            visualObject.transform.SetParent(Entity.Instance.transform, false);
           
            Entity.meshFilter = visualObject.AddComponent<MeshFilter>();
            Entity.meshRenderer = visualObject.AddComponent<MeshRenderer>();
            
            Entity.meshFilter.mesh = R.AssetsLoader<Mesh>.GetAsset(_meshName);
            Entity.meshRenderer.material = R.AssetsLoader<Material>.GetAsset(_materialName);
        }
    }
}