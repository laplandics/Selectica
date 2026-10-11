using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class CellPositionComponent : EntityComponent_d
    {
        private Vector3 _initialPosition;
        
        public CellPositionComponent(ComponentData data) : base(data)
        { _initialPosition = VectorSerializer.FromEntry(data.values[0]); }

        protected override void OnBind() { MoveTo(_initialPosition); }

        public void MoveTo(Vector3 target)
        {
            var from = Entity.Instance.transform.position;
            
            G.Resolve<Cells>().RemoveFromCell(Entity.id, from);
            G.Resolve<Cells>().AddToCell(Entity.id, target);
            
            var grid = Grid.WorldToGrid(target);
            var to = Grid.GridToWorldCenter(grid);
            
            Entity.Instance.transform.position = to;
            Entity.currentCellIndex.Value = new CellIndex(grid.x, grid.y);
        }
    }
}