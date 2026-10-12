using System;
using System.Collections.Generic;

namespace Selectica
{
    public class Systems
    {
        private readonly Dictionary<Type, EntitySystem> _systemsMap = new();
        
        public Systems()
        {
            RegisterSystem<InstanceMaker>();
            RegisterSystem<PrefabLoader>();
            RegisterSystem<MeshMaker>();
            RegisterSystem<CellBinder>();
            RegisterSystem<ColliderMaker>();
            RegisterSystem<SelectionsProvider>();
            RegisterSystem<HarvestProvider>();
            RegisterSystem<BehaviourActivator>();
        }
        
        private void RegisterSystem<T>() where T : EntitySystem, new() => _systemsMap[typeof(T)] = new T(); 
        
        public void AddToSystem<T>(Entity entity, params object[] args) where T : EntitySystem =>
            _systemsMap[typeof(T)].RegisterEntity(entity, args);
        
        public void RemoveFromSystem<T>(Entity entity) where T : EntitySystem =>
            _systemsMap[typeof(T)].UnregisterEntity(entity);
        
        public T GetSystem<T>() where T : EntitySystem => (T)_systemsMap[typeof(T)];
    }
}