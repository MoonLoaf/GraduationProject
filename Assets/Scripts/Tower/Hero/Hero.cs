using System;
using Core;
using UI.Buttons;
using UnityEngine;

namespace Tower.Hero
{
    public class Hero : TowerBase
    {
        private HeroState _state;
        [SerializeField] private float _fishingCooldown = 3.0f;
        [SerializeField] private int _moneyPerFishingtrip;
        private float _lastFishingTime;
        
        private float cooldownTimer = 0.0f; 
        protected override void Start()
        {
            base.Start();
            _state = HeroState.Fishing;
        }

        private void OnEnable()
        {
            AbilityActivationButton.OnAbilityActivated += ActivateAbility;
        }

        private void OnDisable()
        {
            AbilityActivationButton.OnAbilityActivated -= ActivateAbility;
        }

        protected override void Update()
        {
            switch (_state)
            {
                case HeroState.Attacking:
                    base.Update();
                    break;
                case HeroState.Fishing:
                    GoFish();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GoFish()
        {
            if (Time.time - _lastFishingTime <= _fishingCooldown) return;

            _lastFishingTime = Time.time;
            GameManager.Instance.IncrementMoney(_moneyPerFishingtrip);
        }

        protected override void Attack(Vector3 targetPos)
        {
            base.Attack(targetPos);
            Debug.Log("Hero Attack");
            //TODO: Hero attack cool stuff
        }

        public void SetState(HeroState newState)
        {
            _state = newState;
        }

        protected virtual void ActivateAbility()
        {
            throw new NotImplementedException();
        }
    }
}
