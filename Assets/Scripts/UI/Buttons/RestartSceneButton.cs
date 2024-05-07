using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class RestartSceneButton : ClickableButton
    {
        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            string scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    }
}
