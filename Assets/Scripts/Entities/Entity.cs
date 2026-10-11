using System;
using System.Collections.Generic;
using System.Linq;
using Selectica.Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Selectica
{
    [Serializable]
    public class Entity
    {
        public string entityName;
        public string id;
        
        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;
        
        public List<GameObject> colliderObjects = new();
        [SerializeReference] public ReactiveList<ColliderRequest> colliderRequests = new();
        
        [SerializeReference] public Reactive<bool> selected = new();
        [SerializeReference] public Reactive<CellIndex> currentCellIndex = new();
        
        [SerializeReference] public Reactive<bool> harvesting = new();
        [SerializeReference] public Reactive<float> harvestProgress = new();
        
        [SerializeReference] private List<EntityComponent_d> components_d;


        public List<EntityComponent> components;
        
        
        private ComponentData[] _componentsData;

        public EntityInstance Instance { get; private set; }

        public Entity(EntityData data)
        {
            entityName = data.entityName;
            id = Guid.NewGuid().ToString();
            components_d = new List<EntityComponent_d>();
            _componentsData = data.entityComponents;
        }
        
        public void Spawn()
        {
            G.Resolve<Entities>().RegisterEntity(this);
            
            var entityInstance = new GameObject(entityName);
            Instance = entityInstance.AddComponent<EntityInstance>();
            Instance.dataModel = this;

            foreach (var componentData in _componentsData)
            { AddComponent(ComponentsRegistry.CreateInstance(componentData)); }
        }

        public void AddComponent(EntityComponent_d component)
        {
            if (components_d.Contains(component)) return;
            components_d.Add(component);
            component.Bind(this);
        }

        public void RemoveComponent(string componentName, bool removeInstantly)
        {
            var component = components_d.FirstOrDefault(c => c.name == componentName);
            if (component == null) return;

            component.Unbind();
            
            if (!removeInstantly) return;
            components_d.Remove(component);
        }
        
        public void Despawn()
        {
            G.Resolve<Cells>().RemoveFromCell(id, Instance.transform.position);
            components_d.ForEach(c => RemoveComponent(c.name, removeInstantly: false));
            components_d.Clear();
            
            Object.Destroy(Instance.gameObject);
            G.Resolve<Entities>().UnregisterEntity(this);
        }
    }
}