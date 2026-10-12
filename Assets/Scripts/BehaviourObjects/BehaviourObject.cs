using UnityEngine;

namespace Selectica
{
    public abstract class BehaviourObject : MonoBehaviour
    {
        public virtual void Activate() { }
        public virtual void Deactivate() { }
    }
}