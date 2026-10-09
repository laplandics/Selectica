using System;
using System.Collections;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class SelectableComponent : EntityComponent
    {
        public SelectableComponent(ComponentData data) : base(data) { }

        public override IEnumerator Initialize()
        {
            ColliderComponent collider = null;
            yield return new WaitUntil(() => Entity.TryGetComponent(out collider));
            
            var colliderObject = collider.ColliderObject;
            colliderObject.AddComponent<SelectionProvider>().Activate(this);
        }
        
        public void OnSelected() {}
        public void OnDeselected() {}
    }
}