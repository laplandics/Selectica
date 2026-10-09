using System.Collections.Generic;
using Selectica;

public class Entities
{
    private readonly Dictionary<string, Entity> _entitiesMap = new();
    
    public void RegisterEntity(Entity entity) => _entitiesMap.TryAdd(entity.ID, entity);
    public void UnregisterEntity(Entity entity) => _entitiesMap.Remove(entity.ID);

    public Entity GetEntity(string id) => _entitiesMap[id];
}