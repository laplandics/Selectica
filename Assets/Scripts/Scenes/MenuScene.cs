using System.Collections;
using UnityEngine;

namespace Selectica
{
    public class MenuScene : ISceneBoot
    {
        public string SceneName => "Menu";
        
        public IEnumerator Boot()
        {
            yield return G.Resolve<UI>().SetUI(R.MenuUIRoot);
            yield return null;
            
            yield return null;
            
            //DEBUG ONLY
            Debug.LogWarning("DEBUG: Loading game Scene");
            yield return new WaitForSeconds(1.5f);
            yield return G.Resolve<Scenes>().LoadScene(Scenes.GAME);
            //DEBUG ONLY
        }
    }
}