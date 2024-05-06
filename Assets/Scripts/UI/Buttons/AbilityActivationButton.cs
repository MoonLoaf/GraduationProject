using System.Collections;
using Tower.Hero;
using UnityEngine;
using UnityEngine.UI;
using Utility.EnemyWaveLogic;

namespace UI.Buttons
{
    public delegate void AbilityActivationHandler();
    public class AbilityActivationButton : ClickableButton
    {
        public static AbilityActivationHandler OnAbilityActivated;
        [SerializeField] private Image _fillImage;
        private Hero _currentHero;
        
        public override void OnClickInteraction()
        {
            if(!WaveManager.Instance.IsWaveActive){return;}
            OnAbilityActivated?.Invoke();
            StartCoroutine(AbilityCooldown(_currentHero.AbilityCooldown));
        }
        
        protected void OnEnable()
        {
            _button.interactable = false;
            UIEventManager.Instance.HeroSoldEvent += OnHeroSold;
            UIEventManager.Instance.HeroPlacedEvent += OnHeroPlaced;
        }

        protected void OnDisable()
        {
            UIEventManager.Instance.HeroSoldEvent -= OnHeroSold;
            UIEventManager.Instance.HeroPlacedEvent -= OnHeroPlaced;
        }

        private void OnHeroPlaced(Hero hero)
        {
            _currentHero = hero;
            _button.interactable = true;
        }

        private void OnHeroSold(Hero hero)
        {
            _button.interactable = false;
            _currentHero = null;
        }

        private IEnumerator AbilityCooldown(float cooldown)
        {
            _button.interactable = false;
            _fillImage.fillAmount = 0;
            
            float timer = 0;

            while (timer < cooldown)
            {
                timer += Time.deltaTime;
                float fillAmount = Mathf.Clamp01(timer / cooldown);
                _fillImage.fillAmount = fillAmount;
                yield return null;
            }

            _fillImage.fillAmount = 1;
            _button.interactable = true;
        }
    }
}
