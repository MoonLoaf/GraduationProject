using Utility;

namespace UI.Menu
{
    public class MenuLevelStartButton : ClickableButton
    {
        private string _levelToLoadName;
        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            SceneLoader.Instance.LoadSceneAsync(_levelToLoadName);            
        }

        private void OnEnable()
        {
            LevelCardManager.OnLevelToLoadChanged += SetNewLevel;
        }

        private void OnDisable()
        {
            LevelCardManager.OnLevelToLoadChanged -= SetNewLevel;
        }

        private void SetNewLevel(string newLevel)
        {
            _levelToLoadName = newLevel;
        }
    }
}
