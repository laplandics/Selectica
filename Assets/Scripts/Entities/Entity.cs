using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Selectica
{
    [Serializable]
    public class Entity
    {
        [SerializeReference] private List<EntityComponent> components;
        private ComponentData[] _componentsData;
        private string _name;
        
        public string ID { get; private set; } = Guid.NewGuid().ToString();
        public EntityInstance Instance { get; private set; }

        public Entity(EntityData data)
        {
            _name = data.entityName;
            components = new List<EntityComponent>();
            _componentsData = data.entityComponents;
        }
        
        public void Spawn()
        {
            G.Resolve<Entities>().RegisterEntity(this);
            
            var entityInstance = new GameObject(_name);
            Instance = entityInstance.AddComponent<EntityInstance>();
            Instance.dataModel = this;

            foreach (var componentData in _componentsData)
            { AddComponent(ComponentsRegistry.CreateInstance(componentData)); }
        }

        public void AddComponent(EntityComponent component)
        {
            if (components.Contains(component)) return;
            components.Add(component);
            component.Bind(this);
            
            G.Resolve<Coroutines>().Start(component.Initialize());
        }

        public void RemoveComponent(string componentName)
        {
            var component = components.FirstOrDefault(c => c.name == componentName);
            if (component == null) return;
            components.Remove(component);
            component.Unbind();
            
            G.Resolve<Coroutines>().Start(component.Deinitialize());
        }

        public bool TryGetComponent<T>(out T component) where T : EntityComponent
        { component = components.FirstOrDefault(c => c.GetType() == typeof(T)) as T; return component != null; }
        
        public void Despawn()
        {
            components.ForEach(c => RemoveComponent(c.name));
            components.Clear();
            
            Object.Destroy(Instance.gameObject);
            G.Resolve<Entities>().UnregisterEntity(this);
        }
    }
}