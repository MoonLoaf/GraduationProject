using Core;
using Tower;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class TowerPreviewCreatorButton : ClickableButton
    {
        [SerializeField] protected TowerType _typeToSpawn;
        [SerializeField] protected GameObject _towerPreviewPrefab;
        [SerializeField] private Image _imageRef;
        
        
        private bool _previewActive;

        protected virtual void OnEnable()
        {
            _imageRef.sprite = _typeToSpawn.TypeSprite;
            UIEventManager.Instance.TowerPlacedEvent += HandleTowerPlaced;
        }

        protected virtual void OnDisable()
        {
            UIEventManager.Instance.TowerPlacedEvent -= HandleTowerPlaced;
        }

        public override void OnClickInteraction()
        {
            if (UIEventManager.Instance.IsPreviewActive || !GameManager.Instance.CanAfford(_typeToSpawn.Cost)) {return;}

            _button.interactable = false;
            GameObject towerPreviewObject = Instantiate(_towerPreviewPrefab, Vector3.zero, Quaternion.identity);
            TowerPreview towerPreviewComp = towerPreviewObject.GetComponent<TowerPreview>();
            towerPreviewComp.SetType(_typeToSpawn);
            
            UIEventManager.Instance.StartTowerPreview();
        }
        
        private void HandleTowerPlaced()
        {
            _button.interactable = true;
        }
    }
}
