using UnityEngine;

namespace Selectica
{
    public static class EntityConfig
    {
        public static EntityData CameraData(Vector3 targetPosition) => new()
        {
            entityName = "Cam",
            entityComponents = new[]
            {
                ComponentConfig.CameraComponent(targetPosition)
            }
        };

        public static EntityData SoilTileData(Vector3 position, string meshName) => new()
        {
            entityName = "SoilTile",
            entityComponents = new[]
            {
                ComponentConfig.PositionComponent(position),
                ComponentConfig.ColliderComponent(),
                ComponentConfig.VisualComponent("Tile", meshName)
            }
        };

        public static EntityData PlantTileData(Vector3 position, string meshName) => new()
        {
            entityName = "PlantTile",
            entityComponents = new[]
            {
                ComponentConfig.PositionComponent(position),
                ComponentConfig.ColliderComponent(),
                ComponentConfig.VisualComponent("Tile", meshName),
                ComponentConfig.SelectableComponent()
            }
        };
    }
}