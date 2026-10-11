using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class OutlineComponent : EntityComponent_d
    {
        private const float OUTLINE_WIDTH = 6f;
        
        private Outline _outline;
        
        public OutlineComponent(ComponentData data) : base(data) { }

        protected override void OnBind()
        {
            _outline = Entity.Instance.gameObject.AddComponent<Outline>();
            _outline.OutlineWidth = OUTLINE_WIDTH;
            _outline.OutlineColor = Color.lightGreen;
            _outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            _outline.enabled = false;
            Subscriptions.Add(Entity.selected.Subscribe((value, _) => OnSelected(value)));
        }

        private void OnSelected(bool selected) => _outline.enabled = selected;
    }
}