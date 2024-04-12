using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private string _displayedName;
        [SerializeField] private Sprite _sceneSprite;
    }
}
