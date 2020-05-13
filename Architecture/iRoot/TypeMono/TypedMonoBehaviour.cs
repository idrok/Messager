using UnityEngine;

namespace AI.Architecture.iRoot
{
    /// <summary>
    /// If you want to use coroutine, implements like "new public IEnumerator OnMouseDown() { }".
    /// </summary>
    public class TypedMonoBehaviour : MonoBehaviour
    {
        /// <summary>Awake is called when the script instance is being loaded.</summary>
        public virtual void Awake() { }
    }
}