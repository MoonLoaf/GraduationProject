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
            Hero hero = heroObject.GetComponentInChildren<Hero>();
            
            hero.Initialize(_type);
            
            UIEventManager.Instance.NotifyTowerPlaced();
            UIEventManager.Instance.HeroPlacedEvent?.Invoke(hero);
            Destroy(gameObject);
        }
    }
}
