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
            throw new NotImplementedException();
        }

        protected virtual void OnMouseDrag()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnMouseUp()
        {
            throw new NotImplementedException();
        }
    }
}
