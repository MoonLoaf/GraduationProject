using UnityEngine;
using UnityEngine.Splines;
using Utility;
using Utility.EnemyWaveLogic;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyType _type;
        public EnemyType Type => _type;
    
        private int _currentHealth;
        private float _moveSpeed;

        private BoxCollider2D _collider;
        private SpriteRenderer _renderer;
        
        //Testing
        private SplineContainer _spline;
        private float _splineLength;
        private float _distancePercent = 0;
        private bool _shouldMove = false;

        private void Awake()
        {
            _collider = gameObject.AddComponent<BoxCollider2D>();
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        void Start()
        {
            _spline = LevelSpline.Instance.GetLevelSplineContainer();
            _splineLength = _spline.CalculateLength();
        }

        public void Initialize()
        {
            _renderer.sprite = _type.TypeSprite;
            _currentHealth = _type.MaxHP;
            _moveSpeed = _type.MovementSpeed;
            _shouldMove = true;
        }

        void Update()
        {
            if (_spline != null && _shouldMove)
            {
                _distancePercent += _moveSpeed * Time.deltaTime / _splineLength;

                Vector3 currentPos = _spline.EvaluatePosition(_distancePercent);
                transform.position = currentPos;

                if (_distancePercent >= 1f)
                {
                    _distancePercent = 0;
                    _shouldMove = false;
                    WaveManager.Instance.Pool.DespawnEnemy(this);
                }
            }
        }

        public void SetEnemyType(EnemyType newType)
        {
            if(_type != null){return;}
            _type = newType;
        }
    }
}
