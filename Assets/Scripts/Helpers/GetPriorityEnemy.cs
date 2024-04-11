using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Helpers
{
    public static class GetPriorityEnemy
    {
        public static Vector3 First(IReadOnlyList<EnemyBase> enemies)
        {
            var furthestEnemy = enemies[0];
            for (int i = 1; i < enemies.Count; i++)
            {
                furthestEnemy = CompareGreaterPathProgress(furthestEnemy, enemies[i]);
            }

            return furthestEnemy.transform.position;
        }

        public static Vector3 Last(IReadOnlyList<EnemyBase> enemies)
        {
            var lastEnemy = enemies[0];
            for (int i = 1; i < enemies.Count; i++)
            {
                lastEnemy = CompareLeastPathProgress(lastEnemy, enemies[i]);
            }

            return lastEnemy.transform.position;
        }

        public static Vector3 Strongest(IReadOnlyList<EnemyBase> enemies)
        {
            var strongestEnemy = enemies[0];
            for (int i = 1; i < enemies.Count; i++)
            {
                strongestEnemy = CompareStrongest(strongestEnemy, enemies[i]);
            }

            return strongestEnemy.transform.position;
        }

        public static Vector3 Weakest(IReadOnlyList<EnemyBase> enemies)
        {
            var weakestEnemy = enemies[0];
            for (int i = 1; i < enemies.Count; i++)
            {
                weakestEnemy = CompareWeakest(weakestEnemy, enemies[i]);
            }

            return weakestEnemy.transform.position;
        }

        public static Vector3 Closest(IReadOnlyList<EnemyBase> enemies, Vector3 towerPosition)
        {
            var closestEnemy = enemies[0];
            float closestDistance = Vector3.Distance(towerPosition, closestEnemy.transform.position);
            for (int i = 1; i < enemies.Count; i++)
            {
                float currentEnemyDistance = Vector3.Distance(towerPosition, enemies[i].transform.position);
                bool currentEnemyIsCloserThanPreviousClosest = currentEnemyDistance < closestDistance;

                if (!currentEnemyIsCloserThanPreviousClosest) continue;

                closestEnemy = enemies[i];
                closestDistance = currentEnemyDistance;
            }

            return closestEnemy.transform.position;
        }

        private static EnemyBase CompareGreaterPathProgress(EnemyBase enemy1, EnemyBase enemy2)
        {
            if (enemy1.DistanceAlongSpline > enemy2.DistanceAlongSpline)
                return enemy1;
            if (enemy1.DistanceAlongSpline < enemy2.DistanceAlongSpline)
                return enemy2;
            if (enemy1.DistanceAlongSpline > enemy2.DistanceAlongSpline)
                return enemy1;
            if (enemy1.DistanceAlongSpline < enemy2.DistanceAlongSpline)
                return enemy2;
            else
                return enemy1;
        }

        private static EnemyBase CompareLeastPathProgress(EnemyBase enemy1, EnemyBase enemy2)
        {
            if (enemy1.DistanceAlongSpline < enemy2.DistanceAlongSpline)
                return enemy1;
            if (enemy1.DistanceAlongSpline > enemy2.DistanceAlongSpline)
                return enemy2;
            if (enemy1.DistanceAlongSpline < enemy2.DistanceAlongSpline)
                return enemy1;
            if (enemy1.DistanceAlongSpline > enemy2.DistanceAlongSpline)
                return enemy2;
            else
                return enemy1;
        }

        private static EnemyBase CompareStrongest(EnemyBase enemy1, EnemyBase enemy2)
        {
            if (enemy1.GetTotalHealth() > enemy2.GetTotalHealth())
                return enemy1;
            if (enemy1.GetTotalHealth() < enemy2.GetTotalHealth())
                return enemy2;
            
            return CompareGreaterPathProgress(enemy1, enemy2);
        }

        private static EnemyBase CompareWeakest(EnemyBase enemy1, EnemyBase enemy2)
        {
            if (enemy1.GetTotalHealth() < enemy2.GetTotalHealth())
                return enemy1;
            if (enemy1.GetTotalHealth() > enemy2.GetTotalHealth())
                return enemy2;
            
            return CompareGreaterPathProgress(enemy1, enemy2);
        }
    }
}
