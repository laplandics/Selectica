using UnityEngine;
using UnityEngine.InputSystem;

namespace Selectica
{
    public class SelectionState : GameState
    {
        private SelectionProvider _selected;
        
        public override void OnEnter()
        {
            
        }

        public override void OnKeyPressed(InputValues key)
        {
            switch (key)
            {
                case InputValues.MouseKeyLeft: TrySelect(); break;
            }
        }

        private void TrySelect()
        {
            var camera = Camera.main;
            var mouse = Mouse.current;

            if (camera == null || mouse == null) return;
            var ray = camera.ScreenPointToRay(mouse.position.ReadValue());
            var layer = LayerMask.NameToLayer("Selectables");

            if (!Physics.Raycast(ray, out var hit, layer)) return;
            var hitCollider = hit.collider;
            
            if (hitCollider == null) return;
            var selectionProvider = hitCollider.GetComponent<SelectionProvider>();
            
            if (_selected == selectionProvider) { _selected.Deselect(); _selected = null; }
            else { _selected?.Deselect(); _selected = selectionProvider; _selected.Select();}
        }

        public override void OnExit()
        {
            
        }
    }
}