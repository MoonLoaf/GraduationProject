using System;
using System.Collections.Generic;
using Enemy;

namespace Utility.EnemyWaveLogic
{
    [Serializable]
    public struct Wave
    {
        public int EndOfWaveReward;
        public string EndOfWaveText;
        public List<SpawnEvent> SpawnEvents;

        public Wave(List<SpawnEvent> events, int endOfWaveReward, string endOfWaveText)
        { 
            EndOfWaveReward = endOfWaveReward;
            EndOfWaveText = endOfWaveText;
            SpawnEvents = events;
        }
        
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
    }
}
