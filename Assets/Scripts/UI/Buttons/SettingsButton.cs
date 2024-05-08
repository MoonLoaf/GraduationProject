using System;

namespace UI.Buttons
{
    public class SettingsButton : ClickableButton
    {
        private void OnEnable()
        {
            UIEventManager.Instance.OnGameContinue += () => { _button.interactable = true; };
        }

        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            UIEventManager.Instance.OnSettingsPressed?.Invoke();
            _button.interactable = false;
        }
    }
}
