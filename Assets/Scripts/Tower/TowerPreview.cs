using UnityEngine;
using Utility;

namespace Tower
{
    /// <summary>
    /// Class to represent the "hovered" tower while it's being placed
    /// </summary>
    public class TowerPreview : MonoBehaviour
    {
        private TowerType _type;
        private SpriteRenderer _renderer;
        
        private bool _previewActive = true;
        private bool _moved = false;
        private Camera _camera;
        private Vector3 _touchPosition;
        
        private void Awake()
        {
            _renderer = gameObject.AddComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        private void Start()
        {
            _renderer.color = Color.gray;
            _touchPosition = new Vector3();
        }

        public void SetType(TowerType type)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
        }

        private void FixedUpdate()
        {
            if (Input.touchCount > 0 && _previewActive)
            {
                _moved = true;
                Touch touch = Input.GetTouch(0);
                _touchPosition = _camera.ScreenToWorldPoint(touch.position);
                _touchPosition.z = 0;

                if (touch.phase == TouchPhase.Moved)
                {
                    _touchPosition = _camera.ScreenToWorldPoint(touch.position);
                    _touchPosition.z = 0;
                    transform.position = _touchPosition;
                    CheckValidPlacement();
                }
            }

            if (Input.touchCount == 0 && _moved)
            {
                if(!CheckValidPlacement()){return;}
                SpawnTower(_touchPosition);
            }
        }

        private void SpawnTower(Vector3 spawnPos)
        {
            _previewActive = false;
            GameObject towerObject = new GameObject("Tower")
            {
                transform = { position = spawnPos }
            };
            TowerBase tower = towerObject.AddComponent<TowerBase>();
            
            tower.Initialize(_type);
            
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
