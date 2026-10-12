using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Selectica
{
    public class PrefabLoader : EntitySystem
    {
        private readonly Dictionary<Entity, GameObject> _prefabInstancesMap = new();
        
        public override void RegisterEntity(Entity entity, params object[] args)
        {
            var newPrefabName = (string)args[0];

            var instance = G.Resolve<Systems>().GetSystem<InstanceMaker>().GetInstance(entity);
            if (instance == null) throw new Exception($"Entity {entity} has no instance");
            
            if (_prefabInstancesMap.TryGetValue(entity, out var prefabInstance))
            {
                if (prefabInstance.name == newPrefabName) return;
                Object.Destroy(prefabInstance);
            }
            
            var prefab = R.AssetsLoader<GameObject>.GetAsset(newPrefabName);
            var newPrefabInstance = Object.Instantiate(prefab, instance.transform, false);
            newPrefabInstance.name = newPrefabName;
            
            _prefabInstancesMap[entity] = newPrefabInstance;
        }

        public override void UnregisterEntity(Entity entity)
        {
            if (!_prefabInstancesMap.TryGetValue(entity, out var prefabInstance)) return;
            Object.Destroy(prefabInstance);
            _prefabInstancesMap.Remove(entity);
        }
        
        public GameObject GetPrefabInstance(Entity entity) => _prefabInstancesMap.GetValueOrDefault(entity);
    }
}