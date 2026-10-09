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
            
            new Entity(EntityConfig.CameraData(Vector3.zero)).Spawn();
            new Entity(EntityConfig.SoilTileData(Vector3.zero, "DirtTile")).Spawn();
            new Entity(EntityConfig.PlantTileData(Vector3.zero, "GrassTile")).Spawn();
            yield return null;
            
            G.Resolve<States>().ChangeState<SelectionState>();
            yield return null;
        }
    }
}