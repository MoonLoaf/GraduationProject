namespace UI.Buttons
{
    public class SettingsButton : ClickableButton
    {
        public override void OnClickInteraction()
        {
            UIEventManager.Instance.OnSettingsPressed?.Invoke();
        }
    }
}
