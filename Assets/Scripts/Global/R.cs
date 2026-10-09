using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Selectica
{
    public static class R
    {
        public static void Reload()
        {
            AssetsLoader<GameObject>.LoadAssets("Prefabs");
            AssetsLoader<VisualTreeAsset>.LoadAssets("UIAssets");
            AssetsLoader<Material>.LoadAssets("Materials");
            AssetsLoader<Mesh>.LoadAssets("Meshes");
        }
        
        public static PanelSettings UISettings => Resources.Load<PanelSettings>("UIAssets/UISettings");
        public static VisualTreeAsset MenuUIRoot => Resources.Load<VisualTreeAsset>("UIAssets/MenuUIRoot");
        public static VisualTreeAsset GameUIRoot => Resources.Load<VisualTreeAsset>("UIAssets/GameUIRoot");

        public static GameObject CameraPrefab => Resources.Load<GameObject>("Prefabs/GameCamera");
        
        public static class AssetsLoader<T> where T : Object
        {
            private static readonly Dictionary<string, T> AssetsMap = new();

            public static void LoadAssets(string folder)
            {
                AssetsMap.Clear();
                var assets = Resources.LoadAll<T>(folder);
                
                foreach (var asset in assets)
                { AssetsMap.Add(asset.name, asset); }
            }
            
            public static T GetAsset(string name) => AssetsMap[name];
        }
    }
}