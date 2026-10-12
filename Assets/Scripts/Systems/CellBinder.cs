namespace Selectica
{
    public class CellBinder : EntitySystem
    {
        public override void RegisterEntity(Entity entity, params object[] args)
        {
            var newCellIndex = (CellIndex)args[0];
            
            var instance = G.Resolve<Systems>().GetSystem<InstanceMaker>().GetInstance(entity);
            if (instance == null) return;
            
            instance.transform.position = Grid.CellIndexToWorldPosition(newCellIndex);
            G.Resolve<Cells>().AddToCell(entity.id, newCellIndex);
            
            entity.currentCell = G.Resolve<Cells>().GetEntityCell(entity.id);
        }

        public override void UnregisterEntity(Entity entity) { }
    }
}