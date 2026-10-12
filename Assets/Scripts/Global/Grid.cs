using UnityEngine;

namespace Selectica
{
    public static class Grid
    {
        public const float GRID_CELL_SIZE = 1f;
        
        public static Vector3 Snap(Vector3 pos)
        {
            var cellIndex = WorldToGrid(pos);
            var cellCenter = GridToWorldCenter(cellIndex);
            return cellCenter;
        }

        public static Vector2Int WorldToGrid(Vector3 worldPos)
        {
            var x = Mathf.FloorToInt(worldPos.x / GRID_CELL_SIZE);
            var y = Mathf.FloorToInt(worldPos.z / GRID_CELL_SIZE);

            return new Vector2Int(x, y);
        }
        
        public static Vector3 GridToWorldCenter(Vector2Int gridPos)
        {
            const float half = GRID_CELL_SIZE / 2f;

            var x = gridPos.x * GRID_CELL_SIZE + half;
            var z = gridPos.y * GRID_CELL_SIZE + half;

            return new Vector3(x, 0, z);
        }
        
        public static CellIndex GetCellIndex(Vector3 position)
        {
            var cellGridPosition = WorldToGrid(position);
            return new CellIndex(cellGridPosition.x, cellGridPosition.y);
        }
        
        public static Vector3 CellIndexToWorldPosition(CellIndex cellIndex)
        {
            var gridPosition = new Vector2Int(cellIndex.x, cellIndex.z);
            return GridToWorldCenter(gridPosition);
        }
    }
}