using UnityEngine;

namespace UI.Menu
{
    public class LevelSelectionButton : ClickableButton
    {
        [SerializeField] private int _indexChangeOnClick;

        public override void OnClickInteraction()
        {
            LevelCardManager.OnLevelIndexChanged?.Invoke(_indexChangeOnClick);
        }
    }
}
