using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyType _type;
        public EnemyType Type => _type;
    
        private int _currentHealth;
        private float _moveSpeed;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        void Start()
        {
            Reset();
        }

        public void Reset()
        {
            _renderer.sprite = _type.TypeSprite;
            _currentHealth = _type.MaxHP;
            _moveSpeed = _type.MovementSpeed;
        }

        void Update()
        {
        
        }

        public void SetEnemyType(EnemyType newType)
        {
            if(_type != null){return;}
            _type = newType;
        }
    }
}
