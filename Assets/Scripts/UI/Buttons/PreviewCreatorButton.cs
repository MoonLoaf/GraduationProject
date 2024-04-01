using Tower;
using UnityEngine;

namespace UI.Buttons
{
    public class PreviewCreatorButton : ClickableButton
    {
        [SerializeField] private TowerType _typeToSpawn;

        public override void OnClickInteraction()
        {
            GameObject towerPreviewObject = new GameObject("TowerPreview");
            TowerPreview towerPreviewComp = towerPreviewObject.AddComponent<TowerPreview>();
            towerPreviewComp.SetType(_typeToSpawn);
        }
    }
}
