using System;
using UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;
using TouchInput;

namespace Tower
{
    /// <summary>
    /// Class to represent the "hovered" tower while it's being placed
    /// </summary>
    public class TowerPreview : ClickableObject
    {
        [SerializeField] protected GameObject _towerPrefab;
        protected TowerType _type;

        private SpriteRenderer _renderer;
        
        private bool _moved = false;
        private Camera _camera;
        private Vector3 _touchPosition;
        
        private void Awake()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        protected void Start()
        {
            _renderer.color = Color.gray;
            _touchPosition = new Vector3();
        }

        public void SetType(TowerType type)
        {
            _type = type;
            UpdateSprite();
        }
        
        private void OnEnable()
        {
            TouchInputManager.OnTouchStartPosition += OnTouchStart;
            _shaderController.SetRange(_type.Range);
        }

        private void OnDisable()
        {
            TouchInputManager.OnTouchStartPosition -= OnTouchStart;
        }

        private void UpdateSprite()
        {
            _renderer.sprite = _type.TypeSprite;
        }

        public override void OnTouchStart(InputAction.CallbackContext context)
        {
            base.OnTouchStart(context);
            Vector2 touchPosition = context.ReadValue<Vector2>();
            _touchPosition = _camera.ScreenToWorldPoint(touchPosition);
            _touchPosition.z = 0;

            _moved = true;
            MoveTowerPreview();
        }

        private void FixedUpdate()
        {
            if (_moved && Input.touchCount == 0)
            {
                // Touch ended, perform necessary actions
                TryPlaceTower();
            }
        }

        private void TryPlaceTower()
        {
            if (!CheckValidPlacement()) return;
            SpawnTower(_touchPosition);
        }

        private void MoveTowerPreview()
        {
            transform.position = _touchPosition;
            CheckValidPlacement();
        }

        protected virtual void SpawnTower(Vector3 spawnPos)
        {
            GameObject towerObject = Instantiate(_towerPrefab, spawnPos, quaternion.identity);
            TowerBase tower = towerObject.GetComponent<TowerBase>();
            
            tower.Initialize(_type);
            
            UIEventManager.Instance.NotifyTowerPlaced();
            Destroy(gameObject);
        }

        private bool CheckValidPlacement()
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            bool validity = LevelSpline.Instance.CanPlace(pos);
            _renderer.color = validity ? Color.green : Color.red;
            return validity;
        }
    }
}
