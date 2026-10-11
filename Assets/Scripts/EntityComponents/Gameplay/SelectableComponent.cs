using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class SelectableComponent : EntityComponent_d
    {
        public SelectableComponent(ComponentData data) : base(data) { }

        protected override void OnBind()
        {
            Entity.colliderRequests.Add(new ColliderRequest
            { colliderType = ColliderType.SelectionCollider, Callback = OnColliderObjectDone });
            return;
            
            void OnColliderObjectDone(GameObject colliderObject)
            {
                var selectionProvider = colliderObject.AddComponent<SelectionProvider>();
                selectionProvider.BindComponent(this);
            }
        }

        public void OnSelected() {  }
        public void OnDeselected() {  }
    }
}