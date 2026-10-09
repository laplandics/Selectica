using System;
using System.Collections;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class ColliderComponent : EntityComponent
    {
        public ColliderComponent(ComponentData data) : base(data) { }

        public GameObject ColliderObject {get; private set;}

        public override IEnumerator Initialize()
        {
            VisualComponent visual = null;
            yield return new WaitUntil(() => Entity.TryGetComponent(out visual));
            
            var colliderObject = new GameObject("Collider");
            colliderObject.transform.SetParent(Entity.Instance.transform, false);
            
            var meshFilter = visual.MeshFilter;
            var collider = colliderObject.AddComponent<BoxCollider>();
            
            collider.center = meshFilter.sharedMesh.bounds.center;
            collider.size = meshFilter.sharedMesh.bounds.size;
            
            ColliderObject = colliderObject;
        }
    }
}