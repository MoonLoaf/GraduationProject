using TouchInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tower
{
    public class ClickableObject : MonoBehaviour
    {
        protected TouchInputManager _inputManager;

        private void Awake()
        {
            _inputManager = TouchInputManager.Instance;
        }

        public virtual void OnTouchStart(InputAction.CallbackContext context)
        {
            string name = gameObject.name;
            Debug.Log(name + " was touched");
        }
    }
}
