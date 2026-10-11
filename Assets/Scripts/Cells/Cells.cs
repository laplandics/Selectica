using System;
using System.Collections.Generic;
using UnityEngine;

namespace Selectica
{ 
    public class Cells
    {
        private readonly Dictionary<CellIndex, Cell> _cellsMap = new();
        private readonly Dictionary<string, IDisposable[]> _onChangeSubscriptions = new();
        
        public void AddToCell(string entityId, Vector3 position)
        {
            var cellGridPosition = Grid.WorldToGrid(position);
            var cellIndex = new CellIndex(cellGridPosition.x, cellGridPosition.y);
            
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

        public void SubscribeToCellEntitiesChange(Vector3 position, string subscriberId,
            Action<string> onAddCallback = null, Action<string> onRemoveCallback = null)
        {
            var cellGridPosition = Grid.WorldToGrid(position);
            var cellIndex = new CellIndex(cellGridPosition.x, cellGridPosition.y);
            
            if (!_cellsMap.TryGetValue(cellIndex, out var cell))
            { Debug.LogWarning($"Cell with index {cellIndex} not found"); return; }

            if (_onChangeSubscriptions.ContainsKey(subscriberId))
            { Debug.LogWarning($"Subscriber {subscriberId} is already subscribed"); return; }
            
            var subscriptions = new IDisposable[2];
            if (onAddCallback != null) { subscriptions[0] = cell.entities.OnAdd(onAddCallback); }
            if (onRemoveCallback != null) { subscriptions[1] = cell.entities.OnRemove(onRemoveCallback); }
            _onChangeSubscriptions.Add(subscriberId, subscriptions);
        }

        public void UnsubscribeFromCellEntitiesChange(string subscriberId,
            bool unsubscribeFromAdd = true, bool unsubscribeFromRemove = true)
        {
            if (!_onChangeSubscriptions.TryGetValue(subscriberId, out var subscriptions)) return;
            
            if (unsubscribeFromAdd) subscriptions[0]?.Dispose();
            if (unsubscribeFromRemove) subscriptions[1]?.Dispose();
            _onChangeSubscriptions.Remove(subscriberId);
        }
        
        public void RemoveFromCell(string entityId, Vector3 position)
        {
            var cellGridPosition = Grid.WorldToGrid(position);
            var cellIndex = new CellIndex(cellGridPosition.x, cellGridPosition.y);
            
            if (!_cellsMap.TryGetValue(cellIndex, out var cell)) return;
            cell.entities.Remove(entityId);
        }

        public IReadOnlyCollection<string> GetCellEntities(CellIndex cellIndex)
        {
            if (!_cellsMap.TryGetValue(cellIndex, out var cell)) return Array.Empty<string>();
            return cell.entities.Value;
        }
        
        public Cell GetCell(Vector3 position)
        {
            var cellGridPosition = Grid.WorldToGrid(position);
            var cellIndex = new CellIndex(cellGridPosition.x, cellGridPosition.y);
            
            return _cellsMap.GetValueOrDefault(cellIndex);
        }
    }
}