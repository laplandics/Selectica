using UnityEngine;

namespace Selectica
{
    public class SelectionProvider : MonoBehaviour
    {
        private SelectableComponent _selectable;
        
        public void BindComponent(SelectableComponent selectable)
        { _selectable = selectable; }
    }
}