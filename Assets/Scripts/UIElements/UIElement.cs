using System;
using UnityEngine.UIElements;

namespace Selectica
{
    public class UIElement
    {
        private readonly UIBinder _binder;
        private readonly VisualElement _element;

        public string ID { get; }

        public UIElement(UIBinder binder)
        {
            _binder = binder;
            
            var assetName = _binder.GetType().Name;
            var asset = R.AssetsLoader<VisualTreeAsset>.GetAsset(assetName);
            
            if (asset == null) return;
            var templateContainer = asset.Instantiate();
            _element = templateContainer.Q<VisualElement>("UIElement");
            
            _element.RegisterCallback<AttachToPanelEvent>(AttachedCallback);
            _element.RegisterCallback<DetachFromPanelEvent>(DetachedCallback);
            _element.RegisterCallback<GeometryChangedEvent>(GeometryChangedCallback);
            
            ID = Guid.NewGuid().ToString();
            G.Resolve<UI>().RegisterUIElement(ID, this);
            
            _binder.Bind(_element);
        }
        
        public void Add() { _binder.Root.Add(_element); }
        public void Remove() { UnregisterElement(); ClearCallbacks(); _binder.Root.Remove(_element); }
        
        
        private void AttachedCallback(AttachToPanelEvent evt) { _binder.OnAttached(); }
        private void DetachedCallback(DetachFromPanelEvent evt)
        { UnregisterElement(); ClearCallbacks(); _binder.OnDetached(); }
        private void GeometryChangedCallback(GeometryChangedEvent evt) => _binder.OnGeometryChanged();
        
        
        private void ClearCallbacks()
        {
            _element?.UnregisterCallback<AttachToPanelEvent>(AttachedCallback);
            _element?.UnregisterCallback<DetachFromPanelEvent>(DetachedCallback);
            _element?.UnregisterCallback<GeometryChangedEvent>(GeometryChangedCallback);
        }

        private void UnregisterElement() => G.Resolve<UI>().UnregisterUIElement(ID);
    }
}