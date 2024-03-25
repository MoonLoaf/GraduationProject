using UnityEngine;
using UnityEngine.Splines;
using Utility;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyType _type;
        public EnemyType Type => _type;
    
        private int _currentHealth;
        private float _moveSpeed;
        private SpriteRenderer _renderer;
        
        //Testing
        private SplineContainer _spline;
        private float _splineLength;
        private float _moveTime = 0f;
        private float distancePercent = 0;
        
        

        private void Awake()
        {
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        void Start()
        {
            Reset();
            _spline = LevelSpline.Instance.GetLevelSplineContainer();
            _splineLength = _spline.CalculateLength();
        }

        public void Reset()
        {
            _renderer.sprite = _type.TypeSprite;
            _currentHealth = _type.MaxHP;
            _moveSpeed = _type.MovementSpeed;
        }

        void Update()
        {
            if (_spline != null)
            {
                distancePercent += _moveSpeed * Time.deltaTime / _splineLength;

                Vector3 currentPos = _spline.EvaluatePosition(distancePercent);
                transform.position = currentPos;

                if (distancePercent > 1f)
                {
                    Destroy(gameObject);
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
