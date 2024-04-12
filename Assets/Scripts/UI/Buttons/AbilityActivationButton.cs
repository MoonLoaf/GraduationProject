namespace UI.Buttons
{
    public delegate void AbilityActivationHandler();
    public class AbilityActivationButton : ClickableButton
    {
        public static AbilityActivationHandler OnAbilityActivated;
        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            OnAbilityActivated?.Invoke();
        }
    }
}
