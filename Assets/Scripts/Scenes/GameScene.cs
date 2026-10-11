using System.Collections;
using UnityEngine;

namespace Selectica
{
    public class GameScene : ISceneBoot
    {
        public string SceneName => "Game";

        public IEnumerator Boot()
        {
            yield return G.Resolve<UI>().SetUI(R.GameUIRoot);
            yield return null;
            
            new Entity(EntityConfig.CameraData(Grid.Snap(Vector3.zero))).Spawn();
            yield return null;
            
            EntityContainersManager.SetContainers();
            yield return null;
            
            var grinderEntity = new Entity(EntityConfig.GrinderData());
            grinderEntity.Spawn();
            EntityContainersManager.MoveIntoContainer(EntityContainerType.MachineryTilesContainer,
                grinderEntity.Instance.transform);
            yield return null;
            
            // for (var z = -2; z <= 2; z++)
            // {
            //     for (var x = -2; x <= 2; x++)
            //     {
            //         var pos = new Vector3(x, 0, z);
            //         if (pos == Vector3.zero) continue;
            //         
            //         var dirtTileEntity = new Entity(EntityConfig.SoilTileData(pos, "DirtTile"));
            //         var plantTileEntity = new Entity(EntityConfig.PlantTileData(pos, "GrassTile"));
            //         
            //         dirtTileEntity.Spawn();
            //         plantTileEntity.Spawn();
            //         
            //         EntityContainersManager.MoveIntoContainer(EntityContainerType.SoilTilesContainer,
            //             dirtTileEntity.Instance.transform);
            //         
            //         EntityContainersManager.MoveIntoContainer(EntityContainerType.PlantTilesContainer,
            //             plantTileEntity.Instance.transform);
            //         
            //         yield return new WaitForSeconds(0.1f);
            //     }
            // }

            var pos = Vector3.back;
            var dirtTileEntity = new Entity(EntityConfig.SoilTileData(pos, "DirtTile"));
            var plantTileEntity = new Entity(EntityConfig.PlantTileData(pos, "GrassTile"));
            
            dirtTileEntity.Spawn();
            plantTileEntity.Spawn();
            
            EntityContainersManager.MoveIntoContainer(EntityContainerType.SoilTilesContainer,
                dirtTileEntity.Instance.transform);
            
            EntityContainersManager.MoveIntoContainer(EntityContainerType.PlantTilesContainer,
                plantTileEntity.Instance.transform);
            
            var selectorEntity = new Entity(EntityConfig.HarvestCursorData());
            selectorEntity.Spawn();
            
            G.Resolve<States>().ChangeState<SelectionState>();
            yield return null;
        }
    }
}