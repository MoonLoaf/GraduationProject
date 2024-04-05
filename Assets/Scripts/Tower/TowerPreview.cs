using UI;
using Unity.Mathematics;
using UnityEngine;
using Utility;

namespace Tower
{
    /// <summary>
    /// Class to represent the "hovered" tower while it's being placed
    /// </summary>
    public class TowerPreview : MonoBehaviour
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

        private void UpdateSprite()
        {
            _renderer.sprite = _type.TypeSprite;
        }

        private void FixedUpdate()
        {
            if (Input.touchCount > 0)
            {
                _moved = true;
                Touch touch = Input.GetTouch(0);
                _touchPosition = _camera.ScreenToWorldPoint(touch.position);
                _touchPosition.z = 0;

                if (touch.phase == TouchPhase.Moved)
                {
                    MoveTowerPreview();
                }
            }
            else if (Input.touchCount == 0 && _moved)
            {
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
