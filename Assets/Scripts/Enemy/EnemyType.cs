using Audio;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyType", menuName = "EnemyType")]
    public class EnemyType : ScriptableObject
    {
        [Header("Visual")] 
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color[] _layerColors;
        [SerializeField] private Sound _onHitSound;
        [SerializeField] private Sound _onDestroyedSound;
        
    
        [Header("Enemy Stats")]
        [SerializeField] private int _layers;
        [SerializeField] private int _hpPerLayer;
        [SerializeField] private int _rewardPerLayerPopped;
        [SerializeField] private int _enemyPoppedReward;
        [Space]
        [SerializeField] private int _damage;
        [SerializeField] private float _movementSpeed;
    
        [Header("Enemy Flags and Modifiers")]
        [SerializeField] private bool isMetal;
        [SerializeField] private bool isCamo;
    
        public int Layers => _layers;
        public Color[] LayerColors => _layerColors;
        public int HpPerLayer => _hpPerLayer;
        public int RewardPerLayerPopped => _rewardPerLayerPopped;
        public int EnemyPoppedReward => _enemyPoppedReward;
        public float MovementSpeed => _movementSpeed;
        public int Damage => _damage;
        public Sprite TypeSprite => _sprite;
        public Sound OnHitSound => _onHitSound;
        public Sound OnDestroyedSound => _onDestroyedSound;
        public bool IsMetal => isMetal;
        public bool IsCamo => isCamo;
    }
}