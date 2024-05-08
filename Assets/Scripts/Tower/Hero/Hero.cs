using System;
using System.Collections.Generic;
using Core;
using TMPro;
using UI;
using UnityEngine;
using Utility.EnemyWaveLogic;

namespace Tower.Hero
{
    public abstract class Hero : TowerBase
    {
        private HeroState _state;
        [SerializeField] private float _fishingCooldown = 3.0f;
        [SerializeField] private int _moneyPerFishingtrip;
        [Header("Hero Ability")]
        [SerializeField] protected float _abilityDuration;
        [SerializeField] protected float _abilityCooldown;
        public float AbilityCooldown => _abilityCooldown;
        private float _lastFishingTime;

        [SerializeField] private TMP_Dropdown _stateDropdown;
        public bool AbilityReady { get; protected set; } = true;
        
        
        protected override void Start()
        {
            base.Start();
            _state = HeroState.Fishing;
            _stateDropdown.value = (int)_state;
        }

        private void OnEnable()
        {
            UpgradeTab.OnTowerDeselect += HandleTowerDeselect;
            HeroState[] enumValues = (HeroState[])Enum.GetValues(typeof(HeroState));

            _stateDropdown.options.Clear();
            string[] options = new string[enumValues.Length];
            for (int i = 0; i < enumValues.Length; i++)
            {
                options[i] = enumValues[i].ToString();
            }

            _stateDropdown.AddOptions(new List<string>(options));
            _stateDropdown.onValueChanged.AddListener(SetState);
            _stateDropdown.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            UpgradeTab.OnTowerDeselect -= HandleTowerDeselect;
        }

        private void HandleTowerDeselect(bool interaction)
        {
            _stateDropdown.gameObject.SetActive(interaction);
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
            if (!WaveManager.Instance.IsWaveActive) return;

            _lastFishingTime = Time.time;
            GameManager.Instance.IncrementMoney(_moneyPerFishingtrip);
        }

        protected override void Attack(Vector3 targetPos)
        {
            base.Attack(targetPos);
            Debug.Log("Hero Attack");
            //TODO: Hero attack cool stuff
        }

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            if(_stateDropdown.IsActive()){return;}
            _stateDropdown.gameObject.SetActive(true);
        }

        private void SetState(int index)
        {
            _state = (HeroState)index;
            Debug.Log(_state);
            _stateDropdown.value = index;
        }

        public abstract void ActivateAbility();
    }
}
