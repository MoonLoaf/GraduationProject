using Core;

namespace UI.Buttons
{
    public class StartRoundButton : ClickableButton
    {
        private void Start()
        {
            GameManager.Instance.OnWaveEnd += WaveEnd;
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
