using UnityEngine;

namespace Selectica
{
    [SelectionBase]
    public class EntityInstance : MonoBehaviour
    {
        public Entity dataModel;

        private void Awake() { gameObject.hideFlags = HideFlags.NotEditable; }
    }
}