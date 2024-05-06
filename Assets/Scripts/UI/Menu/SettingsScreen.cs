using UnityEngine;

namespace UI.Menu
{
    public class SettingsScreen : MonoBehaviour
    {
        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}
