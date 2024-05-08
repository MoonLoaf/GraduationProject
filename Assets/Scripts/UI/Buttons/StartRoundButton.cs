using Core;

namespace UI.Buttons
{
    public class StartRoundButton : ClickableButton
    {
        private void Start()
        {
            GameManager.Instance.OnWaveEnd += WaveEnd;
        }

        private void OnEnable()
        {
            UIEventManager.Instance.OnSettingsPressed += () => { _button.interactable = false; };
            UIEventManager.Instance.OnGameContinue += () => { _button.interactable = true; };
        }

        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            GameManager.Instance.BeginNewWave();
            _button.interactable = false;
        }

        private void WaveEnd()
        {
            _button.interactable = true;
        }
    }
}
