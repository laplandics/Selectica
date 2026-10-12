using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Selectica
{
    public class SelectionsProvider : EntitySystem
    {
        private readonly Dictionary<Entity, SelectableObject> _selectionsMap = new();
        
        public override void RegisterEntity(Entity entity, params object[] args)
        {
            var isSelectable = (bool)args[0];
            if (_selectionsMap.TryGetValue(entity, out var selectableObject))
            {
                if (isSelectable) return;
                Object.Destroy(selectableObject);
                _selectionsMap.Remove(entity);
            }
            else
            {
                if (!isSelectable) return;
                var selectableColliderObject = G.Resolve<Systems>().GetSystem<ColliderMaker>()
                    .GetSelectionColliderObject(entity);
                if (selectableColliderObject == null) return;
                
                var newSelectableObject = selectableColliderObject.AddComponent<SelectableObject>();
                newSelectableObject.BindProvider(this);
                _selectionsMap[entity] = newSelectableObject;
            }
        }
        
        public override void UnregisterEntity(Entity entity)
        {
            if (!_selectionsMap.TryGetValue(entity, out var selectableObject)) return;
            Object.Destroy(selectableObject);
            _selectionsMap.Remove(entity);
        }

        public void SetEntitySelected(SelectableObject sender)
        {
            var pair = _selectionsMap.FirstOrDefault(vp => vp.Value == sender);
            if (pair.Value == null) return;
            pair.Key.selected.Value = true;
        }

        public void SetEntityDeselected(SelectableObject sender)
        {
            var pair = _selectionsMap.FirstOrDefault(vp => vp.Value == sender);
            if (pair.Value == null) return;
            pair.Key.selected.Value = false;
        }
    }
}