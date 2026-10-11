using System.Collections.Generic;
using Selectica;
using UnityEngine;

public class Entities
{
    private readonly Dictionary<string, Entity> _entitiesMap = new();
    
    public void RegisterEntity(Entity entity) => _entitiesMap.TryAdd(entity.id, entity);
    public void UnregisterEntity(Entity entity) => _entitiesMap.Remove(entity.id);
    
    public Entity GetEntity(string id)
    {
        if (_entitiesMap.TryGetValue(id, out var entity)) return entity;
        Debug.LogWarning($"Couldn't find entity with id: {id}");
        return null;
    }
}