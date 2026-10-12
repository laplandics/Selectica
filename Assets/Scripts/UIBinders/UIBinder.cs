using UnityEngine.UIElements;

namespace Selectica
{
    public abstract class UIBinder
    {
        public abstract VisualElement Root { get; }
        protected VisualElement Element { get; private set; }
        
        public void Bind(VisualElement element)
        { Element = element; Element.dataSource = this; }
        
        public virtual void OnAttached() {}
        public virtual void OnGeometryChanged() {}
        public virtual void OnDetached() {}
    }
}