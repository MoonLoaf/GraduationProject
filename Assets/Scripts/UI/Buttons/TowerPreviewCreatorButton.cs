using Core;
using TMPro;
using Tower;
using Tower.Projectile;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class TowerPreviewCreatorButton : ClickableButton
    {
        [SerializeField] protected TowerType _typeToSpawn;
        [SerializeField] protected GameObject _towerPreviewPrefab;
        [SerializeField] protected Image _imageRef;
        [SerializeField] protected TMP_Text _costText;
        [Space] 
        [SerializeField] private Image _traitImage;
        [SerializeField] private Sprite _explosiveSprite;
        [SerializeField] private Sprite _corrosiveSprite;
        [SerializeField] private Sprite _camoSeeingSprite;
        [SerializeField] private Sprite _punctureSprite;
        [SerializeField] private Sprite _aoeSprite;
        
        private bool _previewActive;

        protected virtual void OnEnable()
        {
            _imageRef.sprite = _typeToSpawn.TypeSprite;
            _costText.text = _typeToSpawn.Cost.ToString();
            SetTraitImg();
            UIEventManager.Instance.TowerPlacedEvent += HandleTowerPlaced;
            UIEventManager.Instance.OnSettingsPressed += () => { _button.interactable = false; };
            UIEventManager.Instance.OnGameContinue += () => { _button.interactable = true; };
        }

        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
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

        protected void SetTraitImg()
        {
            if ((_typeToSpawn.TypeProjectileType.DamageType & DamageType.Explosive) != 0)
            {
                _traitImage.sprite = _explosiveSprite;
                return;
            }
            if ((_typeToSpawn.TypeProjectileType.DamageType & DamageType.Corrosive) != 0)
            {
                _traitImage.sprite = _corrosiveSprite;
                return;
            }
            if ((_typeToSpawn.TypeProjectileType.DamageType & DamageType.Puncture) != 0)
            {
                _traitImage.sprite = _punctureSprite;
                return;
            }
            if (_typeToSpawn.AOETower)
            {
                _traitImage.sprite = _aoeSprite;
                return;
            }
            if (_typeToSpawn.CamoSeeing)
            {
                _traitImage.sprite = _camoSeeingSprite;
                return;
            }
            //else
            _traitImage.enabled = false;
        }
    }
}
