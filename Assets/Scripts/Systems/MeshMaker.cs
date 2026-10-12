using System.Collections.Generic;
using UnityEngine;

namespace Selectica
{
    public class MeshMaker : EntitySystem
    {
        private readonly Dictionary<Entity, MeshInfo> _meshInfoMap = new();
        
        public override void RegisterEntity(Entity entity, params object[] args)
        {
            var newMeshInfo = (MeshInfo)args[0];

            if (_meshInfoMap.TryGetValue(entity, out var meshInfo))
            {
                var meshObject = meshInfo.MeshObject;
                
                var meshFilter = meshObject.GetComponent<MeshFilter>();
                var meshRenderer = meshObject.GetComponent<MeshRenderer>();

                if (meshFilter.mesh.name != newMeshInfo.meshName)
                {
                    var newMesh = R.AssetsLoader<Mesh>.GetAsset(newMeshInfo.meshName);
                    meshFilter.mesh = newMesh;
                }

                if (meshRenderer.sharedMaterial.name != newMeshInfo.materialName)
                {
                    var newMaterial = R.AssetsLoader<Material>.GetAsset(newMeshInfo.materialName);
                    meshRenderer.sharedMaterial = newMaterial;
                }
                
                _meshInfoMap[entity] = newMeshInfo;
            }
            else
            {
                var instance = G.Resolve<Systems>().GetSystem<InstanceMaker>().GetInstance(entity);
                if (instance == null) return;
                
                var mesh = R.AssetsLoader<Mesh>.GetAsset(newMeshInfo.meshName);
                var material = R.AssetsLoader<Material>.GetAsset(newMeshInfo.materialName);

                var meshObject = new GameObject("Visual");
                meshObject.transform.SetParent(instance.transform, false);
                
                var meshFilter = meshObject.AddComponent<MeshFilter>();
                var meshRenderer = meshObject.AddComponent<MeshRenderer>();
                
                meshFilter.mesh = mesh;
                meshRenderer.sharedMaterial = material;
                
                newMeshInfo.MeshObject = meshObject;
                _meshInfoMap[entity] = newMeshInfo;
            }
        }

        public override void UnregisterEntity(Entity entity)
        {
            if (!_meshInfoMap.TryGetValue(entity, out var meshInfo)) return;
            Object.Destroy(meshInfo.MeshObject);
            _meshInfoMap.Remove(entity);
        }

        public GameObject GetMeshObject(Entity entity) => !_meshInfoMap.TryGetValue(entity,
            out var meshInfo) ? null : meshInfo.MeshObject;
    }
}