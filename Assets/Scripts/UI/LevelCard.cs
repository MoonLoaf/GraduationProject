using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelCard : MonoBehaviour
    {
        [SerializeField] private Image _imageRef;
        [SerializeField] private TMP_Text _nameText;

        public void Initialize(Sprite sprite, string levelName)
        {
            _imageRef.sprite = sprite;
            _nameText.text = levelName;
        }
    }
}
