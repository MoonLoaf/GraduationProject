using System;
using System.Collections.Generic;

[Serializable]
public struct Wave
{
    [Serializable]
    public struct SpawnEvent
    {
        public EnemyType Type;
        public int Count;
        public float StartSpawnTime; 
        public float SpawnTickRate;

        public SpawnEvent(EnemyType type, int count, float time, float spawnTickRate)
        {
            Type = type;
            Count = count;
            StartSpawnTime = time;
            SpawnTickRate = spawnTickRate;
        }
    }

    public List<SpawnEvent> SpawnEvents;

    public Wave(List<SpawnEvent> events)
    {
        SpawnEvents = events;
    }
}
