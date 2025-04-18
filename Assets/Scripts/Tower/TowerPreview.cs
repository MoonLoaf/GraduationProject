using System.Collections.Generic;
using Audio;
using Core;
using Helpers;
using UI;
using Unity.Mathematics;
using UnityEngine;
using Utility;
using Button = UnityEngine.UI.Button;

namespace Tower
{
    /// <summary>
    /// Class to represent the "hovered" tower while it's being placed
    /// </summary>
    public class TowerPreview : MonoBehaviour
    {
        [SerializeField] protected GameObject _towerPrefab;
        [SerializeField] protected GameObject _AOEtowerPrefab;
        [SerializeField] private Button _cancelButton;
        private bool _spawnAOE;
        
        protected TowerType _type;

        private SpriteRenderer _renderer;

        private bool _moved = false;
        private Camera _camera;
        private Vector3 _touchPosition;
        private CircleCollider2D _collider;
        private ContactFilter2D _filter;
        private RangeShaderController _shaderController;

        protected virtual void Awake()
        {
            _shaderController = GetComponentInChildren<RangeShaderController>();
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _collider = GetComponent<CircleCollider2D>();
            _camera = Camera.main;
            _cancelButton.onClick.AddListener(OnPreviewCanceled);
            _filter.layerMask = Layers.Towers;
        }


        protected void Start()
        {
            _renderer.color = Color.gray;
            _touchPosition = new Vector3();
            
            _shaderController.SetRange(_type.Range);
            _shaderController.SetDisplayRange(true);
            _spawnAOE = _type.AOETower;
        }

        public void SetType(TowerType type)
        {
            _type = type;
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            _renderer.sprite = _type.TypeSprite;
        }

        protected void OnMouseDown()
        {
            Vector2 touchPosition = Input.touches[0].position;
            _touchPosition = _camera.ScreenToWorldPoint(touchPosition);
            _touchPosition.z = 0;

            _moved = true;
            MoveTowerPreview();
        }

        protected void OnMouseDrag()
        {
            Vector2 touchPosition = Input.touches[0].position;
            _touchPosition = _camera.ScreenToWorldPoint(touchPosition);
            _touchPosition.z = 0;

            _moved = true;
            MoveTowerPreview();
        }

        protected void OnMouseUp()
        {
            if(!_moved){return;}
            TryPlaceTower();
        }

        private void TryPlaceTower()
        {
            if (!CheckValidPlacement()) return;
            if (!CheckForAdjacentTurrets()) return;
            GameManager.Instance.DecrementMoney(_type.Cost);
            SpawnTower(_touchPosition);
        }

        private bool CheckForAdjacentTurrets()
        {
            List<Collider2D> results = new List<Collider2D>();
            bool validity = Physics2D.OverlapCollider(_collider, _filter, results) <= 0;
            _renderer.color = validity ? Color.green : Color.red;
            return validity;
        }

        private void MoveTowerPreview()
        {
            transform.position = _touchPosition;
            CheckValidPlacement();
        }

        protected virtual void SpawnTower(Vector3 spawnPos)
        {
            if (_spawnAOE)
            {
                GameObject towerObject = Instantiate(_AOEtowerPrefab, spawnPos, quaternion.identity);
                TowerBase tower = towerObject.GetComponent<TowerBase>();
                tower.Initialize(_type);
            }
            else
            {
                GameObject towerObject = Instantiate(_towerPrefab, spawnPos, quaternion.identity);
                TowerBase tower = towerObject.GetComponent<TowerBase>();
                tower.Initialize(_type);
            }
            
            UIEventManager.Instance.NotifyTowerPlaced();
            Destroy(gameObject);
        }

        private bool CheckValidPlacement()
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            bool validity = LevelSpline.Instance.CanPlace(pos, _collider.radius);
            _renderer.color = validity ? Color.green : Color.red;
            return validity;
        }
        
        private void OnPreviewCanceled()
        {
            UIEventManager.Instance.NotifyTowerPlaced();
            AudioManager.Instance.Play("ButtonClick");
            Destroy(gameObject);
        }
    }
}
