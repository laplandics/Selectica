using System.Collections.Generic;

namespace Selectica
{
    public class BehaviourActivator : EntitySystem
    {
        private readonly Dictionary<Entity, BehaviourObject> _behaviourObjectsMap = new ();
        
        public override void RegisterEntity(Entity entity, params object[] args)
        {
            var behaviourActive = (bool)args[0];

            if (_behaviourObjectsMap.TryGetValue(entity, out var behaviourObject))
            {
                if (behaviourActive) behaviourObject.Activate();
                else behaviourObject.Deactivate();
            }
            else
            {
                if (!behaviourActive) return;
                var prefabInstance = G.Resolve<Systems>().GetSystem<PrefabLoader>().GetPrefabInstance(entity);
                if (prefabInstance == null) return;
                
                var newBehaviourObject = prefabInstance.GetComponent<BehaviourObject>();
                newBehaviourObject.Activate();
                _behaviourObjectsMap[entity] = newBehaviourObject;
            }
        }

        public override void UnregisterEntity(Entity entity)
        {
            if (!_behaviourObjectsMap.TryGetValue(entity, out var behaviourObject)) return;
            behaviourObject?.Deactivate();
            _behaviourObjectsMap.Remove(entity);
        }
    }
}