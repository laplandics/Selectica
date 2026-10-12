using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Selectica
{
    public class Scenes
    {
        private const string BOOT = "Boot";
        public const string GAME = "Game";
        public const string MENU = "Menu";

        private readonly Dictionary<string, ISceneBoot> _sceneMaps = new();
        
        public void RegisterScene<T>(T scene) where T : ISceneBoot
        { _sceneMaps.Add(scene.SceneName, scene); }
        
        public IEnumerator LoadScene(string sceneName)
        {
            if (!_sceneMaps.TryGetValue(sceneName, out var sceneBoot))
            { Debug.LogError($"Scene {sceneName} does not exist"); yield break; }
            
            yield return SceneManager.LoadSceneAsync(BOOT);
            yield return null;
            
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return null;

            yield return sceneBoot.Boot();
            yield return null;
        }
    }
}
