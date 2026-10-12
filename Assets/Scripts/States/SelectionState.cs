using System;
using System.Collections;
using UnityEngine.InputSystem;

namespace Selectica
{
    public class SelectionState : GameState
    {
        private bool _isDragging;
        
        public override void OnEnter()
        { G.Resolve<Inputs>().SwitchInputs(InputMapType.Gameplay); }

        public override void OnKeyPressed(InputValueType key)
        {
            if (key != InputValueType.MouseKeyLeft) return;
            if (_isDragging) return;
           
            G.Resolve<Coroutines>().Start(EnterHarvestStateRoutine());
        }
        
        private IEnumerator EnterHarvestStateRoutine()
        {
            _isDragging = true;
            var startPos = Mouse.current.position.ReadValue();
            
            while (Mouse.current.leftButton.isPressed)
            {
                var delta = Mouse.current.position.ReadValue() - startPos;
                
                if (Math.Abs(delta.x) > 25f || Math.Abs(delta.y) > 25f)
                {
                    _isDragging = false;
                    EnterHarvestState();
                    yield break;
                }
                
                yield return null;
            }
            
            _isDragging = false;
        }
        
        private void EnterHarvestState() => G.Resolve<States>().ChangeState<HarvestState>();

        public override void OnExit() { }
    }
}