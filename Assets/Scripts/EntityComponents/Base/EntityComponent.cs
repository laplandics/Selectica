using System;
using System.Collections;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public abstract class EntityComponent
    {
        [HideInInspector] public string name;
        protected Entity Entity;
        protected ComponentData Data;
        
        protected EntityComponent(ComponentData data) { Data = data; }
        
        public void Bind(Entity entity) { name = GetType().Name; Entity = entity; OnBind();}
        public void Unbind() { OnUnbind(); }
        protected virtual void OnBind() {}
        protected virtual void OnUnbind() {}
        
        public virtual IEnumerator Initialize() { yield break; }
        public virtual IEnumerator Deinitialize() { yield break; }
    }
}