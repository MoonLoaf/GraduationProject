using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Hero.ConcreteHeroes
{
    public class Fisherman : Hero
    {
        private float lastAbilityTime;
        private WaitForSeconds _abilityDurationWait;
        private Dictionary<TowerBase, float> _originalAttackSpeeds = new();

        protected override void Awake()
        {
            base.Awake();
            _abilityDurationWait = new WaitForSeconds(_abilityDuration);
        }

        protected override void ActivateAbility()
        {
            if (Time.time - _lastAttackTime < _abilityCooldown) return;
            
            foreach (var tower in _towersInRange)
            {
                _originalAttackSpeeds[tower] = tower.CurrentType.AttackSpeed;
                tower.CurrentType.AttackSpeed *= 0.8f;
            }

            StartCoroutine(RemoveBuffs());
        }

        private IEnumerator RemoveBuffs()
        {
            yield return _abilityDurationWait;
            
            foreach (var tower in _towersInRange)
            {
                if(!_originalAttackSpeeds.TryGetValue(tower, out var speed)) { continue; }
                tower.CurrentType.AttackSpeed = speed;
            }
            _originalAttackSpeeds.Clear();
        }
    }
}
