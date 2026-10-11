using UnityEngine;

namespace Selectica
{
    public static class ComponentConfig
    {
        public static ComponentData ColliderComponent() => new()
        { componentName = nameof(Selectica.ColliderComponent), values = new string[] { } };
        
        public static ComponentData CellPositionComponent(Vector3 value) => new()
        { componentName = nameof(Selectica.CellPositionComponent), values = new[] { VectorSerializer.ToEntry(value) } };
        
        public static ComponentData SelectableComponent() => new()
        { componentName = nameof(Selectica.SelectableComponent), values = new string[] { } };
        
        public static ComponentData MeshComponent(string materialName, string meshName) => new()
        { componentName = nameof(Selectica.MeshComponent), values = new[] {  materialName, meshName } };
        
        public static ComponentData HarvestableComponent() => new()
        { componentName = nameof(Selectica.HarvestableComponent), values = new string[] { } };
        
        public static ComponentData OutlineComponent() => new()
        { componentName = nameof(Selectica.OutlineComponent), values = new string[] { } };

        
        public static ComponentData CameraComponent(Vector3 targetPosition) => new()
        { componentName = nameof(Selectica.CameraComponent), values = new[] { VectorSerializer.ToEntry(targetPosition) } };

        public static ComponentData HarvestCursorComponent() => new()
        { componentName = nameof(Selectica.HarvestCursorComponent), values = new string[] { } };
    }
}