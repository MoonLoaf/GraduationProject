namespace UI.Buttons
{
    public class SettingsButton : ClickableButton
    {
        public override void OnClickInteraction()
        {
            UIEventManager.OnSettingsPressed?.Invoke();
        }
    }
}
