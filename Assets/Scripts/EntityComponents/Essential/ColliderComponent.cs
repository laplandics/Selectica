using System;
using System.Collections;
using UnityEngine;

namespace Selectica
{
    public enum ColliderType { SelectionCollider, HarvestCollider }
    
    [Serializable]
    public class ColliderComponent : EntityComponent_d
    {
        public ColliderComponent(ComponentData data) : base(data) { }

        protected override void OnBind()
        {
            Subscriptions.Add(Entity.colliderRequests.OnAdd(value =>
            { Coroutines.Add(G.Resolve<Coroutines>().Start(MakeCollider(value), Entity.Instance)); }));
            
            foreach (var colliderRequest in Entity.colliderRequests.Value)
            { Coroutines.Add(G.Resolve<Coroutines>().Start(MakeCollider(colliderRequest), Entity.Instance)); }
        }

        private IEnumerator MakeCollider(ColliderRequest colliderRequest)
        {
            yield return new WaitUntil(() => Entity.meshFilter != null);
            
            var colliderObject = new GameObject(colliderRequest.colliderType.ToString());
            colliderObject.transform.SetParent(Entity.Instance.transform, false);
            
            switch (colliderRequest.colliderType)
            {
                case ColliderType.SelectionCollider:
                    MakeSelectionCollider(colliderObject);
                    break;
                
                case ColliderType.HarvestCollider:
                    MakeHarvestCollider(colliderObject);
                    break;
            }
            
            Entity.colliderObjects.Add(colliderObject);
            colliderRequest.Callback?.Invoke(colliderObject);
            
            Entity.colliderRequests.Remove(colliderRequest);
        }

        private void MakeSelectionCollider(GameObject colliderObject)
        {
            var selectableLayer = LayerMask.NameToLayer("Selectable");
            colliderObject.layer = selectableLayer;
            
            var meshFilter = Entity.meshFilter;
            var collider = colliderObject.AddComponent<BoxCollider>();
            
            collider.center = meshFilter.sharedMesh.bounds.center;
            collider.size = meshFilter.sharedMesh.bounds.size;
        }

        private void MakeHarvestCollider(GameObject colliderObject)
        {
            var harvestLayer = LayerMask.NameToLayer("Harvestable");
            colliderObject.layer = harvestLayer;
            
            var meshFilter = Entity.meshFilter;
            var collider = colliderObject.AddComponent<SphereCollider>();
            
            var center = meshFilter.sharedMesh.bounds.center;
            center.y = 0f;
            
            collider.center = center;
            collider.radius = 0.02f;
        }
    }
}