namespace UI.Buttons
{
    public class SettingsButton : ClickableButton
    {
        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            UIEventManager.Instance.OnSettingsPressed?.Invoke();
        }
    }
}
