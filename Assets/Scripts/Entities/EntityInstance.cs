using UnityEngine;

namespace Selectica
{
    [SelectionBase]
    public class EntityInstance : MonoBehaviour
    {
        public Entity entity;

        private void Awake()
        {
            gameObject.hideFlags = HideFlags.NotEditable;
        }
    }
}