using System;
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

        private void OnEnable()
        {
            UIEventManager.Instance.TowerPlacedEvent += HandleTowerPlaced;
        }

        private void OnDisable()
        {
            UIEventManager.Instance.TowerPlacedEvent -= HandleTowerPlaced;
        }

        public override void OnClickInteraction()
        {
            if (UIEventManager.Instance.IsPreviewActive) {return;}

            GameObject towerPreviewObject = Instantiate(_towerPreviewPrefab, Vector3.zero, Quaternion.identity);
            TowerPreview towerPreviewComp = towerPreviewObject.GetComponent<TowerPreview>();
            towerPreviewComp.SetType(_typeToSpawn);
            
            UIEventManager.Instance.StartTowerPreview();
        }
        
        private void HandleTowerPlaced()
        {
            //Not needed now but maybe for sounds and stuff
        }
    }
}
