using UnityEngine;

namespace Selectica
{
    public class SelectionProvider : MonoBehaviour
    {
        private SelectableComponent _selectable;
        public void Activate(SelectableComponent selectable)
        { _selectable = selectable; gameObject.layer = LayerMask.NameToLayer("Selectables"); }

        public void Select() => _selectable.OnSelected();
        public void Deselect() => _selectable.OnDeselected();
    }
}