using Enemy;
using Helpers;
using UnityEngine;

namespace Tower
{
    public delegate void EnemyInRangeHandler(EnemyBase enemy); 
    public delegate void TowerInRangeHandler(TowerBase tower); 
    
    public class EntityDetector : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _rangeCollider;
        public event EnemyInRangeHandler OnNewEnemyInRange;
        public event EnemyInRangeHandler OnEnemyOutOfRange;
        public event TowerInRangeHandler OnNewTowerInRange;
        public event TowerInRangeHandler OnTowerOutOfRange;

        public void SetRange(float range)
        {
            _rangeCollider.radius = range;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.IsOnLayer(Layers.Enemies))
            {
                OnNewEnemyInRange?.Invoke(other.GetComponent<EnemyBase>());
                return;
            }

            if (other.IsOnLayer(Layers.Towers))
            {
                OnNewTowerInRange?.Invoke(other.GetComponent<TowerBase>());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.IsOnLayer(Layers.Enemies))
            {
                OnEnemyOutOfRange?.Invoke(other.GetComponent<EnemyBase>());
                return;
            }

            if (other.IsOnLayer(Layers.Towers))
            {
                OnTowerOutOfRange?.Invoke(other.GetComponent<TowerBase>());
            }
        }
    }
}
