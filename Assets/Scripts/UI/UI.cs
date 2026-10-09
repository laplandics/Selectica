using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Selectica
{
    public class UI
    {
        private VisualElement _root;
        private readonly PanelRenderer _renderer;
        private readonly Dictionary<string, UIElement> _uiElementsMap = new();

        private int _uiVersion;
        
        public VisualElement UIRoot => _root.Q<VisualElement>("ROOT");

        public UI()
        {
            var rendererObject = new GameObject("[UI]");
            
            _renderer = rendererObject.AddComponent<PanelRenderer>();
            _renderer.panelSettings = R.UISettings;
            
            Object.DontDestroyOnLoad(rendererObject);
        }
        
        public IEnumerator SetUI(VisualTreeAsset root)
        {
            _root = null;
            
            _renderer.UnregisterUIReloadCallback(OnUIReload);
            _renderer.RegisterUIReloadCallback(OnUIReload);
            
            _renderer.visualTreeAsset = root;
            yield return new WaitUntil(() => _root != null);
        }
        
        private void OnUIReload(PanelRenderer param0, VisualElement root, int version)
        { if (version == _uiVersion) return; _uiVersion = version; _root = root; }
        
        public void RegisterUIElement(string id, UIElement element) => _uiElementsMap.Add(id, element);
        public void UnregisterUIElement(string id) => _uiElementsMap.Remove(id);
        
        public UIElement GetUIElement(string id) => _uiElementsMap[id];
    }
}