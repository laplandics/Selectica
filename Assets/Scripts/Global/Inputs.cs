using System.Collections;
using Selectica.Tools;
using UnityEngine.InputSystem;

namespace Selectica
{
    public enum InputValueType
    {
        None,
        MouseKeyLeft, MouseKeyRight, MouseKeyMiddle,
    }
    
    public enum InputMapType { None, Gameplay }
    
    public class Inputs
    {
        public readonly Reactive<InputValueType> PressedKey = new();
        
        private readonly InputActions _inputs = new();
        private InputMapType _currentMapType;
        
        public InputAction CameraInclineAction {get; private set;}

        public void Activate()
        {
            _inputs.Enable();

            CameraInclineAction = _inputs.Camera.Incline;
            
            SwitchInputs(InputMapType.None);
            G.Resolve<Coroutines>().Start(InputsDetectionRoutine());
        }

        public void SwitchInputs(InputMapType mapType)
        {
            _inputs.Disable();
            switch (mapType)
            {
                case InputMapType.None: break;
                case InputMapType.Gameplay: _inputs.Camera.Enable(); break;
            }
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

                if (mouse.leftButton.isPressed)
                {
                    if (PressedKey.Value != InputValueType.MouseKeyLeft)
                    { PressedKey.Value = InputValueType.MouseKeyLeft; }
                }
                else if (mouse.rightButton.isPressed)
                {
                    if (PressedKey.Value != InputValueType.MouseKeyRight)
                    { PressedKey.Value = InputValueType.MouseKeyRight; }
                }
                else if (mouse.middleButton.isPressed)
                {
                    if (PressedKey.Value != InputValueType.MouseKeyMiddle)
                    { PressedKey.Value = InputValueType.MouseKeyMiddle; }
                }
                else
                {
                    if (PressedKey.Value != InputValueType.None)
                    { PressedKey.Value = InputValueType.None; }
                }
                
                yield return null;
            }
        }
    }
}