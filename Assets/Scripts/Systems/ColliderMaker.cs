using System.Collections.Generic;
using UnityEngine;

namespace Selectica
{
    public class ColliderMaker : EntitySystem
    {
        private readonly Dictionary<Entity, CollidersInfo> _collidersInfoMap = new();
        
        public override void RegisterEntity(Entity entity, params object[] args)
        {
            var newCollidersInfo = (CollidersInfo)args[0];

            if (_collidersInfoMap.TryGetValue(entity, out var collidersInfo))
            {
                if (collidersInfo.hasSelectionCollider != newCollidersInfo.hasSelectionCollider)
                {
                    if (collidersInfo.hasSelectionCollider) Object.Destroy(collidersInfo.SelectionColliderObject);
                    else
                    {
                        newCollidersInfo.SelectionColliderObject = MakeSelectionCollider(entity);
                    }
                }

                if (collidersInfo.hasHarvestCollider != newCollidersInfo.hasHarvestCollider)
                {
                    if (collidersInfo.hasHarvestCollider) Object.Destroy(collidersInfo.HarvestColliderObject);
                    else
                    {
                        newCollidersInfo.HarvestColliderObject = MakeHarvestCollider(entity);
                    }
                }
                
                _collidersInfoMap[entity] = newCollidersInfo;
            }
            else
            {
                if (newCollidersInfo.hasSelectionCollider)
                {
                    newCollidersInfo.SelectionColliderObject = MakeSelectionCollider(entity);
                }

                if (newCollidersInfo.hasHarvestCollider)
                {
                    newCollidersInfo.HarvestColliderObject = MakeHarvestCollider(entity);
                }
                
                _collidersInfoMap[entity] = newCollidersInfo;
            }
        }

        private GameObject MakeSelectionCollider(Entity entity)
        {
            var (instance, meshObject) = GetInstanceAndMeshObj(entity);
            
            var colliderObject = new GameObject("SelectionCollider");
            colliderObject.transform.SetParent(instance.transform, false);
            colliderObject.layer = LayerMask.NameToLayer("Selectable");
            
            var meshFilter = meshObject.GetComponent<MeshFilter>();
            var mesh = meshFilter.sharedMesh;
            
            var collider = colliderObject.AddComponent<BoxCollider>();
            
            collider.center = mesh.bounds.center;
            collider.size = mesh.bounds.size;

            return colliderObject;
        }

        private GameObject MakeHarvestCollider(Entity entity)
        {
            var (instance, meshObject) = GetInstanceAndMeshObj(entity);
            
            var colliderObject = new GameObject("HarvestCollider");
            colliderObject.transform.SetParent(instance.transform, false);
            colliderObject.layer = LayerMask.NameToLayer("Harvestable");
            
            var meshFilter = meshObject.GetComponent<MeshFilter>();
            var collider = colliderObject.AddComponent<SphereCollider>();
            
            var center = meshFilter.sharedMesh.bounds.center;
            center.y = 0f;
            
            collider.center = center;
            collider.radius = 0.02f;
            
            return colliderObject;
        }

        private (EntityInstance instance, GameObject meshObject) GetInstanceAndMeshObj(Entity entity)
        {
            var instance = G.Resolve<Systems>().GetSystem<InstanceMaker>().GetInstance(entity);
            if (instance == null) return (null, null);
            
            var meshObject = G.Resolve<Systems>().GetSystem<MeshMaker>().GetMeshObject(entity);
            if (meshObject == null) return (instance, null);
            
            return (instance, meshObject);
        }
        
        public override void UnregisterEntity(Entity entity)
        {
            if (!_collidersInfoMap.TryGetValue(entity, out var collidersInfo)) return;
            if (collidersInfo.hasSelectionCollider) Object.Destroy(collidersInfo.SelectionColliderObject);
            if (collidersInfo.hasHarvestCollider) Object.Destroy(collidersInfo.HarvestColliderObject);
            _collidersInfoMap.Remove(entity);
        }

        public GameObject GetSelectionColliderObject(Entity entity)
        {
            if (!_collidersInfoMap.TryGetValue(entity, out var collidersInfo)) return null;
            if (!collidersInfo.hasSelectionCollider) return null;
            return collidersInfo.SelectionColliderObject;
        }

        public GameObject GetHarvestColliderObject(Entity entity)
        {
            if (!_collidersInfoMap.TryGetValue(entity, out var collidersInfo)) return null;
            if (!collidersInfo.hasHarvestCollider) return null;
            return collidersInfo.HarvestColliderObject;
        }
    }
}