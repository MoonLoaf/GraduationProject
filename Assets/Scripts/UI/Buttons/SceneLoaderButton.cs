using UnityEngine;
using Utility;

namespace UI.Buttons
{
    public class SceneLoaderButton : ClickableButton
    {
        [SerializeField] private string _levelToLoadName;
        
        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            SceneLoader.Instance.LoadSceneAsync(_levelToLoadName);            
        }
    }
}
