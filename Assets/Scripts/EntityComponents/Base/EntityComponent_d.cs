using System;
using System.Collections.Generic;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public abstract class EntityComponent_d
    {
        [HideInInspector] public string name;
        protected Entity Entity;
        protected ComponentData Data;
        
        protected List<IDisposable> Subscriptions = new();
        protected List<Coroutine> Coroutines = new();
        
        protected EntityComponent_d(ComponentData data) { Data = data; }
        
        public void Bind(Entity entity) { name = GetType().Name; Entity = entity; OnBind();}

        public void Unbind()
        {
            Subscriptions.ForEach(s => s?.Dispose());
            Subscriptions.Clear();
            
            Coroutines.ForEach(c => { if(c != null) G.Resolve<Coroutines>().Stop(c, Entity.Instance); });
            Coroutines.Clear();
            OnUnbind();
        }
        
        protected virtual void OnBind() {}
        protected virtual void OnUnbind() {}
    }
}