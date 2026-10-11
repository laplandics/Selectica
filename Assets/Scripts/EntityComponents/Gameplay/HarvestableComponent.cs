using System;
using System.Collections;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class HarvestableComponent : EntityComponent_d
    {
        private const float HARVEST_SPEED = 4f;
        private Coroutine _harvestProgressionRoutine;
        
        public HarvestableComponent(ComponentData data) : base(data) { }
        
        protected override void OnBind()
        {
            Entity.colliderRequests.Add(new ColliderRequest
            { colliderType = ColliderType.HarvestCollider, Callback = OnColliderObjectDone });
            return;

            void OnColliderObjectDone(GameObject colliderObject)
            { colliderObject.AddComponent<HarvestProvider>().BindComponent(this); }
        }

        public void BeginHarvest()
        {
            Entity.selected.Value = true;
            Entity.harvesting.Value = true;
            
            _harvestProgressionRoutine = G.Resolve<Coroutines>().Start(
                HarvestProgressionRoutine(), Entity.Instance);
            Coroutines.Add(_harvestProgressionRoutine);
        }

        private IEnumerator HarvestProgressionRoutine()
        {
            while (Main.IsPlaying && Entity.harvestProgress.Value < 5f)
            { Entity.harvestProgress.Value += Time.deltaTime * HARVEST_SPEED; yield return null; }
            FinishHarvest();
        }
        
        public void EndHarvest()
        {
            Entity.selected.Value = false;
            Entity.harvesting.Value = false;
            
            if (_harvestProgressionRoutine != null) G.Resolve<Coroutines>().Stop(
                _harvestProgressionRoutine, Entity.Instance);
            Coroutines.Remove(_harvestProgressionRoutine);
            Entity.harvestProgress.Value = 0f;
        }

        private void FinishHarvest()
        {
            Entity.selected.Value = false;
            Entity.harvesting.Value = false;
            
            Entity.Despawn();
        }
    }
}