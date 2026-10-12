using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Selectica
{
    public class HarvestProvider : EntitySystem
    {
        private readonly Dictionary<Entity, HarvestableObject> _harvestsMap = new();
        
        public override void RegisterEntity(Entity entity, params object[] args)
        {
            var isHarvestable = (bool)args[0];

            if (_harvestsMap.TryGetValue(entity, out var harvestableObject))
            {
                if (isHarvestable) return;
                Object.Destroy(harvestableObject);
                _harvestsMap.Remove(entity);
            }
            else
            {
                if (!isHarvestable) return;
                var harvestableColliderObject = G.Resolve<Systems>().GetSystem<ColliderMaker>()
                    .GetHarvestColliderObject(entity);
                if (harvestableColliderObject == null) return;
                
                var newHarvestableObject = harvestableColliderObject.AddComponent<HarvestableObject>();
                newHarvestableObject.BindProvider(this);
                _harvestsMap[entity] = newHarvestableObject;
            }
        }

        public override void UnregisterEntity(Entity entity)
        {
            if (!_harvestsMap.TryGetValue(entity, out var harvestableObject)) return;
            Object.Destroy(harvestableObject);
            _harvestsMap.Remove(entity);
        }

        public void MarkEntityHarvesting(HarvestableObject sender)
        {
            var pair = _harvestsMap.FirstOrDefault(vp => vp.Value == sender);
            if (pair.Value == null) return;
            pair.Key.harvesting.Value = true;
        }

        public void UnmarkEntityHarvesting(HarvestableObject sender)
        {
            var pair = _harvestsMap.FirstOrDefault(vp => vp.Value == sender);
            if (pair.Value == null) return;
            pair.Key.harvesting.Value = false;
        }
    }
}