using System.Collections.Generic;

namespace Selectica
{
    public abstract class EntitySystem
    {
        public readonly List<string> RegisteredEntities = new();
        
        public bool Active => RegisteredEntities.Count > 0;
        
        public virtual void RegisterEntity(string entityId) { RegisteredEntities.Add(entityId); }
        public virtual void UnRegisterEntity(string entityId) { RegisteredEntities.Remove(entityId); }
    }
}