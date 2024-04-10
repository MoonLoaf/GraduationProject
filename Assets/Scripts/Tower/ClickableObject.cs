using TouchInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tower
{
    public class ClickableObject : MonoBehaviour
    {
        protected TouchInputManager _inputManager;
        protected RangeShaderController _shaderController;
        protected virtual void Awake()
        {
            _inputManager = TouchInputManager.Instance;
            _shaderController = GetComponent<RangeShaderController>();
        }

        public virtual void OnTouchStart(InputAction.CallbackContext context)
        {
            string name = gameObject.name;
            Debug.Log(name + " was touched");
            //_shaderController.SetDisplayRange(true);
        }
    }
}
