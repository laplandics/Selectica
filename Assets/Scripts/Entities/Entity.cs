using System;
using System.Collections.Generic;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class Entity : IDisposable
    {
        public string id;
        
        [SerializeReference] public Reactive<bool> hasInstance = new(false);
        [SerializeReference] public Reactive<MeshInfo> meshInfo = new(default);
        [SerializeReference] public Reactive<CellIndex> cellIndex = new(default);
        [SerializeReference] public Reactive<CollidersInfo> colliders = new(default);
        
        [SerializeReference] public Reactive<bool> selectable = new(false);
        [SerializeReference] public Reactive<bool> harvestable = new(false);
        [SerializeReference] public Reactive<bool> selected = new(false);
        [SerializeReference] public Reactive<bool> harvesting = new(false);
        
        [SerializeReference] public Reactive<string> prefabName = new(string.Empty);
        [SerializeReference] public Reactive<bool> hasBehaviour = new(false);
        public Cell currentCell;
        
        private List<IDisposable> _subscriptions = new();
        
        public Entity()
        {
            id = Guid.NewGuid().ToString();
            
            Sub<InstanceMaker, bool>(hasInstance);
            Sub<PrefabLoader, string>(prefabName);
            Sub<MeshMaker, MeshInfo>(meshInfo);
            Sub<CellBinder, CellIndex>(cellIndex);
            Sub<ColliderMaker, CollidersInfo>(colliders);
            Sub<SelectionsProvider, bool>(selectable);
            Sub<HarvestProvider, bool>(harvestable);
            Sub<BehaviourActivator, bool>(hasBehaviour);
        }
        
        public void Dispose()
        {
            UnSub<InstanceMaker>();
            UnSub<PrefabLoader>();
            UnSub<MeshMaker>();
            UnSub<CellBinder>();
            UnSub<ColliderMaker>();
            UnSub<SelectionsProvider>();
            UnSub<HarvestProvider>();
            UnSub<BehaviourActivator>();
            
            _subscriptions.ForEach(x => x?.Dispose());
        }

        private void Sub<T, TE>(Reactive<TE> r) where T : EntitySystem =>
        _subscriptions.Add(r.SubscribeSilently((nV, oV) => G.Resolve<Systems>().AddToSystem<T>(this, nV, oV)));
        private void UnSub<T>() where T : EntitySystem => G.Resolve<Systems>().RemoveFromSystem<T>(this);
    }
}