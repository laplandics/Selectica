using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Selectica
{
    [Serializable]
    public class CameraComponent : EntityComponent_d
    {
        private const float INCLINE_SPEED = 100.0f;
        
        private Vector3 _targetPosition;
        private CinemachineOrbitalFollow _cmOrbitalFollow;
        
        public CameraComponent(ComponentData data) : base(data)
        { _targetPosition = VectorSerializer.FromEntry(data.values[0]); }

        protected override void OnBind()
        {
            var cameraPrefab = R.CameraPrefab;
            var cameraObject = Object.Instantiate(cameraPrefab, Entity.Instance.transform, false);
            cameraObject.name = "GameCamera";
            
            var target = cameraObject.transform.Find("Target");
            target.localPosition = _targetPosition;
            
            _cmOrbitalFollow = cameraObject.GetComponentInChildren<CinemachineOrbitalFollow>();

            G.Resolve<Coroutines>().Start(CameraInclineRoutine(), Entity.Instance);
        }

        private IEnumerator CameraInclineRoutine()
        {
            var inclineInput = G.Resolve<Inputs>().CameraInclineAction;
            var mouse = Mouse.current;

            var inclineMax = _cmOrbitalFollow.VerticalAxis.Range.y;
            var inclineMin = _cmOrbitalFollow.VerticalAxis.Range.x;
            
            while (Main.IsPlaying)
            {
                if (!mouse.rightButton.isPressed)
                { yield return null; continue; }
                
                var inclineValueRaw = inclineInput.ReadValue<float>();
                var inclineValue = Mathf.Clamp(inclineValueRaw, -1, 1);
                var incline = inclineValue * INCLINE_SPEED * Time.deltaTime;
                
                var currentIncline = _cmOrbitalFollow.VerticalAxis.Value;
                _cmOrbitalFollow.VerticalAxis.Value = Mathf.Clamp(
                    currentIncline + incline, inclineMin, inclineMax);
                yield return null;
            }
        }
    }
}