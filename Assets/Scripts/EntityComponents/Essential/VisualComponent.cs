using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class VisualComponent : EntityComponent
    {
        private string _materialName;
        private string _meshName;
        
        public MeshFilter MeshFilter {get; private set;}

        public VisualComponent(ComponentData data) : base(data)
        { _materialName = data.values[0]; _meshName = data.values[1]; }

        protected override void OnBind()
        {
            var visualObject = new GameObject("Visual");
            visualObject.transform.SetParent(Entity.Instance.transform, false);
           
            var meshFilter = visualObject.AddComponent<MeshFilter>();
            var meshRenderer = visualObject.AddComponent<MeshRenderer>();
            
            meshFilter.mesh = R.AssetsLoader<Mesh>.GetAsset(_meshName);
            meshRenderer.material = R.AssetsLoader<Material>.GetAsset(_materialName);
            
            MeshFilter = meshFilter;
        }
    }
}