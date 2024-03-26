using UnityEngine;
using UnityEngine.Serialization;

namespace Tower
{
    [CreateAssetMenu(fileName = "TowerType", menuName = "TowerType")]
    public class TowerType : ScriptableObject
    {
        [Header("Visual")] 
        [SerializeField] private Sprite _sprite;
    
        [Header("Tower Stats")]
        [SerializeField] private int _damage;
        [SerializeField] private float _attackSpeed;
    
        [FormerlySerializedAs("isExplosive")]
        [Header("Enemy Flags and Modifiers")]
        [SerializeField] private bool _isExplosive;
        [SerializeField] private bool _isCorrosive;
        [SerializeField] private bool _isPuncture;
        // TODO: Add more modifiers
    
        public int Damage => _damage;
        public float AttackSpeed => _attackSpeed;
        public Sprite TypeSprite => _sprite;
    
        public bool IsExplosive => _isExplosive;
        public bool IsCorrosive => _isCorrosive;
        public bool IsPuncture => _isPuncture;
    }
}