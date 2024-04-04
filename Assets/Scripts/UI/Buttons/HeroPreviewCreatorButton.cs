using Tower;
using UnityEngine;

namespace UI.Buttons
{
    public class HeroPreviewCreatorButton : ClickableButton
    {
        [SerializeField] private TowerType _typeToSpawn;

        public override void OnClickInteraction()
        {
            GameObject towerPreviewObject = new GameObject("HeroPreview");
            HeroPreview towerPreviewComp = towerPreviewObject.AddComponent<HeroPreview>();
            towerPreviewComp.SetType(_typeToSpawn);
        }
    }
}
