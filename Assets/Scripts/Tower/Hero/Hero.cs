using System;
using UI.Buttons;
using UnityEngine;

namespace Tower.Hero
{
    public class Hero : TowerBase
    {
        private HeroState _state;

        private void Start()
        {
            _state = HeroState.Hybrid;
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
                case HeroState.Hybrid:
                    if (_enemiesInRange.Count > 0)
                    {
                        //Make stats worse
                        base.Update();
                        break;
                    }
                    GoFish();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GoFish()
        {
            //TODO: implement economy
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
