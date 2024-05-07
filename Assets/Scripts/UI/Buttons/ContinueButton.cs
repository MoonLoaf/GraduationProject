namespace UI.Buttons
{
    public class ContinueButton : ClickableButton
    {
        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            UIEventManager.Instance.OnGameContinue?.Invoke();
        }
    }
}
