using Tower.Hero;
using UnityEngine;

namespace UI.Buttons
{
    public class HeroPreviewCreatorButton : TowerPreviewCreatorButton
    {
        public override void OnClickInteraction()
        {
            if (UIEventManager.Instance.IsPreviewActive) {return;}

            GameObject heroPreviewObject = Instantiate(_towerPreviewPrefab, Vector3.zero, Quaternion.identity);
            HeroPreview heroPreviewComp = heroPreviewObject.GetComponent<HeroPreview>();
            heroPreviewComp.SetType(_typeToSpawn);
            _button.interactable = false;
            
            UIEventManager.Instance.StartTowerPreview();
        }
    }
}
