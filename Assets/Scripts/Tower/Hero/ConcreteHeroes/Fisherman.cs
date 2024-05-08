using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Hero.ConcreteHeroes
{
    public class Fisherman : Hero
    {
        private WaitForSeconds _abilityDurationWait;
        private WaitForSeconds _abilityCooldownWait;
        private Dictionary<TowerBase, float> _originalAttackSpeeds = new();

        protected override void Awake()
        {
            base.Awake();
            _abilityDurationWait = new WaitForSeconds(_abilityDuration);
            _abilityCooldownWait = new WaitForSeconds(_abilityCooldown);
        }

        public override void ActivateAbility()
        {
            AbilityReady = false;
            Debug.Log("ability");
            
            foreach (var tower in _towersInRange)
            {
                _originalAttackSpeeds[tower] = tower.CurrentType.AttackSpeed;
                tower.CurrentType.AttackSpeed *= 0.8f;
            }
            Debug.Log(_originalAttackSpeeds.Count);

            StartCoroutine(RemoveBuffs());
            StartCoroutine(AbilityCooldownCoroutine());
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

        private IEnumerator AbilityCooldownCoroutine()
        {
            yield return _abilityCooldownWait;
            AbilityReady = true;
        }
    }
}
