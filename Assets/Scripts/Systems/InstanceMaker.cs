using System.Collections.Generic;
using UnityEngine;

namespace Selectica
{
    public class InstanceMaker : EntitySystem
    {
        private readonly Dictionary<Entity, EntityInstance> _instancesMap = new();
        
        public override void RegisterEntity(Entity entity, params object[] args)
        {
            var hasInstance = (bool)args[0];

            if (_instancesMap.TryGetValue(entity, out var existingInstance))
            {
                if (hasInstance) return;
                Object.Destroy(existingInstance.gameObject);
            }
            else
            {
                if (!hasInstance) return;
                
                var instanceObject = new GameObject("Entity");
                var instance = instanceObject.AddComponent<EntityInstance>();
                instance.entity = entity;
                
                _instancesMap[entity] = instance;
            }
        }

        public override void UnregisterEntity(Entity entity)
        {
            if (!_instancesMap.TryGetValue(entity, out var existingInstance)) return;
            Object.Destroy(existingInstance.gameObject);
            _instancesMap.Remove(entity);
        }
        
        public EntityInstance GetInstance(Entity entity) => _instancesMap[entity];
    }
}