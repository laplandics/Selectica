using UnityEngine;

namespace Selectica
{
    public class HarvestableObject : MonoBehaviour
    {
        private HarvestProvider _provider;
        
        public void BindProvider(HarvestProvider provider)
        {
            _provider = provider;
        }

        [Button]
        public void BeginHarvest()
        {
            _provider.MarkEntityHarvesting(this);
        }

        [Button]
        public void StopHarvest()
        {
            _provider.UnmarkEntityHarvesting(this);
        }
    }
}