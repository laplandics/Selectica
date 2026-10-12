using UnityEngine;

namespace Selectica
{
    public class SelectableObject : MonoBehaviour
    {
        private SelectionsProvider _provider;

        public void BindProvider(SelectionsProvider provider)
        {
            _provider = provider;
        }
        
        [Button]
        public void Select()
        {
            _provider.SetEntitySelected(this);
        }
        
        [Button]
        public void Deselect()
        {
            _provider.SetEntityDeselected(this);
        }
    }
}