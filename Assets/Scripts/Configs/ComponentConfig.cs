using UnityEngine;

namespace Selectica
{
    public static class ComponentConfig
    {
        public static ComponentData ColliderComponent() => new()
        { componentName = nameof(ColliderComponent), values = new string[] { } };
        
        public static ComponentData PositionComponent(Vector3 value) => new()
        { componentName = nameof(PositionComponent), values = new[] { Vector3Serializer.ToEntry(value) } };
        
        public static ComponentData SelectableComponent() => new()
        { componentName = nameof(SelectableComponent), values = new string[] { } };
        
        public static ComponentData VisualComponent(string materialName, string meshName) => new()
        { componentName = nameof(VisualComponent), values = new[] {  materialName, meshName } };
        
        public static ComponentData CameraComponent(Vector3 targetPosition) => new()
        { componentName = nameof(CameraComponent), values = new[] { Vector3Serializer.ToEntry(targetPosition) } };
    }
}