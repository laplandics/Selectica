using System.Collections;

namespace Selectica
{
    public class GameScene : ISceneBoot
    {
        public string SceneName => "Game";

        public IEnumerator Boot()
        {
            yield return G.Resolve<UI>().SetUI(R.GameUIRoot);
            yield return null;
            
            var camEntity = new Entity();
            camEntity.hasInstance.Value = true;
            camEntity.cellIndex.Value = new CellIndex(0, 0);
            camEntity.prefabName.Value = "GameCamera";
            camEntity.hasBehaviour.Value = true;
            
            var grinderEntity = new Entity();
            grinderEntity.hasInstance.Value = true;
            grinderEntity.meshInfo.Value = new MeshInfo("Grinder", "Tile");
            grinderEntity.colliders.Value = new CollidersInfo(hasSelectionCollider: true, hasHarvestCollider: false);
            grinderEntity.selectable.Value = true;
            grinderEntity.cellIndex.Value = new CellIndex(0, 0);
            
            var dirtTileEntity = new Entity();
            dirtTileEntity.hasInstance.Value = true;
            dirtTileEntity.meshInfo.Value = new MeshInfo("DirtTile", "Tile");
            dirtTileEntity.cellIndex.Value = new CellIndex(0, -1);
            
            var grassTileEntity = new Entity();
            grassTileEntity.hasInstance.Value = true;
            grassTileEntity.meshInfo.Value = new MeshInfo("GrassTile", "Tile");
            grassTileEntity.colliders.Value = new CollidersInfo(hasSelectionCollider: true, hasHarvestCollider: true);
            grassTileEntity.selectable.Value = true;
            grassTileEntity.harvestable.Value = true;
            grassTileEntity.cellIndex.Value = new CellIndex(0, -1);
            
            G.Resolve<States>().ChangeState<SelectionState>();
            yield return null;
        }
    }
}