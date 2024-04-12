using UI;
using Unity.Mathematics;
using UnityEngine;
using Utility;

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

        protected bool _moved = false;
        protected Camera _camera;
        protected Vector3 _touchPosition;
        private CircleCollider2D _collider;
        
        protected override void Awake()
        {
            base.Awake();
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _collider = GetComponent<CircleCollider2D>();
            _camera = Camera.main;
        }

        protected void Start()
        {
            _renderer.color = Color.gray;
            _touchPosition = new Vector3();
            
            _shaderController.SetRange(_type.Range);
            _shaderController.SetDisplayRange(true);
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

        protected override void OnMouseDown()
        {
            Vector2 touchPosition = Input.touches[0].position;
            _touchPosition = _camera.ScreenToWorldPoint(touchPosition);
            _touchPosition.z = 0;

            _moved = true;
            MoveTowerPreview();
        }

        protected override void OnMouseDrag()
        {
            Vector2 touchPosition = Input.touches[0].position;
            _touchPosition = _camera.ScreenToWorldPoint(touchPosition);
            _touchPosition.z = 0;

            _moved = true;
            MoveTowerPreview();
        }

        protected override void OnMouseUp()
        {
            if(!_moved){return;}
            TryPlaceTower();
        }

        protected void TryPlaceTower()
        {
            if (!CheckValidPlacement()) return;
            SpawnTower(_touchPosition);
        }

        protected void MoveTowerPreview()
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
            bool validity = LevelSpline.Instance.CanPlace(pos, _collider.radius);
            _renderer.color = validity ? Color.green : Color.red;
            return validity;
        }
    }
}
