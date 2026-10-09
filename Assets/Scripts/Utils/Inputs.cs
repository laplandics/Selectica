using System.Collections;
using Selectica.Tools;
using UnityEngine.InputSystem;

namespace Selectica
{
    public enum InputValues { MouseKeyLeft, MouseKeyRight }
    
    public class Inputs
    {
        public readonly Reactive<InputValues> PressedKey = new();
        private readonly InputActions _inputs = new();

        public void Activate()
        {
            _inputs.Enable();
            G.Resolve<Coroutines>().Start(InputsDetectionRoutine());
        }

        private IEnumerator InputsDetectionRoutine()
        {
            var mouse = Mouse.current;
            var keyboard = Keyboard.current;
            
            while (Main.IsPlaying)
            {
                if (keyboard == null || mouse == null)
                {
                    keyboard = Keyboard.current;
                    mouse = Mouse.current;
                    yield return null;
                    continue;
                }

                if (mouse.leftButton.wasPressedThisFrame) PressedKey.Value = InputValues.MouseKeyLeft;
                if (mouse.rightButton.wasPressedThisFrame) PressedKey.Value = InputValues.MouseKeyRight;
                
                yield return null;
            }
        }
    }
}