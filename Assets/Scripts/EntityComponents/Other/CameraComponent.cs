using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Selectica
{
    [Serializable]
    public class CameraComponent : EntityComponent
    {
        private Vector3 _targetPosition;

        public CameraComponent(ComponentData data) : base(data)
        { _targetPosition = Vector3Serializer.FromEntry(data.values[0]); }

        protected override void OnBind()
        {
            var cameraPrefab = R.CameraPrefab;
            var cameraObject = Object.Instantiate(cameraPrefab, Entity.Instance.transform, false);
            cameraObject.name = "GameCamera";
            
            var target = cameraObject.transform.Find("Target");
            target.localPosition = _targetPosition;
        }
    }
}