using System;
using UnityEngine;

namespace Tower
{
    public class ClickableObject : MonoBehaviour
    {
        protected RangeShaderController _shaderController;
        protected virtual void Awake()
        {
            _shaderController = GetComponent<RangeShaderController>();
        }
        
        protected virtual void OnMouseDown()
        {
        }

        protected virtual void OnMouseDrag()
        {
        }

        protected virtual void OnMouseUp()
        {
        }
    }
}
