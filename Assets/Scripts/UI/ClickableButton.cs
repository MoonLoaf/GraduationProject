using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ClickableButton : MonoBehaviour, IClickable
    {
        protected Button _button;
        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickInteraction);
        }

        public virtual void OnClickInteraction()
        {
            throw new System.NotImplementedException();
        }
    }
}
