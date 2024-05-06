namespace UI.Buttons
{
    public class ContinueButton : ClickableButton
    {
        public override void OnClickInteraction()
        {
            UIEventManager.Instance.OnGameContinue?.Invoke();
        }
    }
}
