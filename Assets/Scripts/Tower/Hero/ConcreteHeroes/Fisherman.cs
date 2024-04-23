using System.Collections;
using UnityEngine;

namespace Tower.Hero.ConcreteHeroes
{
    public class Fisherman : Hero
    {
        [Header("Hero Ability")]
        [SerializeField] private float _abilityDuration;
        [SerializeField] private float _abilityCooldown;
        private float lastAbilityTime;
        private WaitForSeconds _abilityWait;

        protected override void Awake()
        {
            base.Awake();
            _abilityWait = new WaitForSeconds(_abilityCooldown);
        }

        protected override void ActivateAbility()
        {
            if (Time.time - _lastAttackTime < _abilityCooldown) return;
            
            foreach (var tower in _towersInRange)
            {
                //TODO: Add an upgrade
            }

            StartCoroutine(RemoveBuffs());
        }

        private IEnumerator RemoveBuffs()
        {
            yield return _abilityWait;
            
            foreach (var tower in _towersInRange)
            {
                //TODO: Remove upgrade
            }
        }
    }
}
