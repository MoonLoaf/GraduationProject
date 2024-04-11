using Enemy;
using Helpers;
using UnityEngine;

namespace Tower
{
    public delegate void EnemyInRangeHandler(EnemyBase enemy); 
    
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _rangeCollider;
        
        public event EnemyInRangeHandler OnNewEnemyInRange;
        public event EnemyInRangeHandler OnEnemyOutOfRange;

        public void SetRange(float range)
        {
            _rangeCollider.radius = range;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.IsNotOnLayer(Layers.Enemies)) { return; }
            OnNewEnemyInRange?.Invoke(other.GetComponent<EnemyBase>());
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.IsNotOnLayer(Layers.Enemies)) { return; }

            OnEnemyOutOfRange?.Invoke(other.GetComponent<EnemyBase>());
        }
    }
}
