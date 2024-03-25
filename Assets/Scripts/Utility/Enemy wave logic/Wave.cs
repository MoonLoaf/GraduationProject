using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine.Serialization;

namespace Utility.EnemyWaveLogic
{
    [Serializable]
    public struct Wave
    {
        [Serializable]
        public struct SpawnEvent
        {
            public EnemyType Type;
            public int Count;
            public float Delay; 
            public float SpawnTickRate;

            public SpawnEvent(EnemyType type, int count, float delay, float spawnTickRate)
            {
                Type = type;
                Count = count;
                Delay = delay;
                SpawnTickRate = spawnTickRate;
            }
        }

        public List<SpawnEvent> SpawnEvents;

        public Wave(List<SpawnEvent> events)
        {
            SpawnEvents = events;
        }
    }
}
