using UnityEngine;

namespace Tower
{
    public class TowerBase : MonoBehaviour
    {
        [SerializeField] private TowerType _type;

        public TowerType Type => _type;

        private float _attackSpeed;
        private float _damage;
        private float _range;
        
        private BoxCollider2D _collider;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _collider = gameObject.AddComponent<BoxCollider2D>();
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        public void Initialize(TowerType type)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _attackSpeed = _type.AttackSpeed;
            _damage = _type.Damage;
            _range = _type.Range;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}
