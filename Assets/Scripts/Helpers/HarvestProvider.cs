using UnityEngine;

namespace Selectica
{
    public class HarvestProvider : MonoBehaviour
    {
        private HarvestableComponent _harvestable;
        
        public void BindComponent(HarvestableComponent harvestable)
        { _harvestable = harvestable; }

        public void StartHarvest() => _harvestable.BeginHarvest();

        public void StopHarvest() => _harvestable.EndHarvest();
    }
}