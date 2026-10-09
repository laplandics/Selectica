using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class PositionComponent : EntityComponent
    {
        private readonly Vector3 _position;
        
        public PositionComponent(ComponentData data) : base(data)
        { _position = Vector3Serializer.FromEntry(data.values[0]); }

        protected override void OnBind()
        { Entity.Instance.transform.position = _position; }
    }
}