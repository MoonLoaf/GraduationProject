using Tower;
using Tower.Hero;
using UnityEngine;

public class HeroPreview : TowerPreview
{
    protected override void SpawnTower(Vector3 spawnPos)
    {
        _previewActive = false;
        GameObject heroObject = new GameObject("Hero")
        {
            transform = { position = spawnPos }
        };
        Hero hero = heroObject.AddComponent<Hero>();
            
        hero.Initialize(_type);
            
        Destroy(gameObject);
    }
}
