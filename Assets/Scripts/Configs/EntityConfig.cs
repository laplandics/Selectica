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

        public static EntityData HarvestCursorData() => new()
        {
            entityName = "HarvestCursor",
            entityComponents = new[]
            {
                ComponentConfig.HarvestCursorComponent(),
            }
        };
        
        
        public static EntityData SoilTileData(Vector3 position, string meshName) => new()
        {
            entityName = "SoilTile",
            entityComponents = new[]
            {
                ComponentConfig.CellPositionComponent(position),
                ComponentConfig.MeshComponent("Tile", meshName),
            }
        };

        public static EntityData PlantTileData(Vector3 position, string meshName) => new()
        {
            entityName = "PlantTile",
            entityComponents = new[]
            {
                ComponentConfig.CellPositionComponent(position),
                ComponentConfig.SelectableComponent(),
                ComponentConfig.HarvestableComponent(),
                ComponentConfig.ColliderComponent(),
                ComponentConfig.MeshComponent("Tile", meshName),
                ComponentConfig.OutlineComponent()
            }
        };

        public static EntityData GrinderData() => new()
        {
            entityName = "Grinder",
            entityComponents = new[]
            {
                ComponentConfig.CellPositionComponent(Vector3.zero),
                ComponentConfig.ColliderComponent(),
                ComponentConfig.MeshComponent("Tile", "Grinder"),
                ComponentConfig.SelectableComponent()
            }
        };
    }
}