using UI;
using Unity.Mathematics;
using UnityEngine;

namespace Tower.Hero
{
    public class HeroPreview : TowerPreview
    {
        protected override void SpawnTower(Vector3 spawnPos)
        {
            GameObject heroObject = Instantiate(_towerPrefab, spawnPos, quaternion.identity);
            Hero hero = heroObject.GetComponent<Hero>();
            
            hero.Initialize(_type);
            
            UIEventManager.Instance.NotifyTowerPlaced();
            Destroy(gameObject);
        }
    }
}
