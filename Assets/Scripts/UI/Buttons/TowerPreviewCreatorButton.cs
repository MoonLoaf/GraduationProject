using System;
using Core;
using Tower;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Buttons
{
    public class TowerPreviewCreatorButton : ClickableButton
    {
        [SerializeField] protected TowerType _typeToSpawn;
        [SerializeField] protected GameObject _towerPreviewPrefab;
        
        private bool _previewActive;

        protected virtual void OnEnable()
        {
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
