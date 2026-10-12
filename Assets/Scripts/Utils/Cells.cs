using System;
using System.Collections.Generic;
using System.Linq;

namespace Selectica
{ 
    public class Cells
    {
        private readonly Dictionary<CellIndex, Cell> _cellsMap = new();
        private readonly Dictionary<string, IDisposable[]> _onChangeSubscriptions = new();

        public void AddToCell(string entityId, CellIndex cellIndex)
        {
            if (_cellsMap.TryGetValue(cellIndex, out var cell))
            {
                cell.entities.AddUnique(entityId);
            }
            else
            {
                cell = new Cell(cellIndex);
                cell.entities.AddUnique(entityId);
                _cellsMap.Add(cellIndex, cell);
            }
        }
        
        public void RemoveFromCell(string entityId, CellIndex cellIndex)
        {
            if (!_cellsMap.TryGetValue(cellIndex, out var cell)) return;
            cell.entities.Remove(entityId);
        }

        public Cell GetEntityCell(string entityId)
        {
            foreach (var (_, cell) in _cellsMap)
            {
                if (!cell.entities.Value.Contains(entityId)) continue;
                return cell;
            }
            
            return null;
        }
    }
}
