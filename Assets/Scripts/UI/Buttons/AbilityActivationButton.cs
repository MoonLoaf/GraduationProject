using System.Collections;
using Tower.Hero;
using UnityEngine;
using UnityEngine.UI;
using Utility;

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
            OnAbilityActivated?.Invoke();
            StartCoroutine(AbilityCooldown(_currentHero.AbilityCooldown));
            Logging.Log("Ability Activated");
        }
        
        protected void OnEnable()
        {
            _button.interactable = false;
            UIEventManager.HeroSoldEvent += OnHeroSold;
            UIEventManager.HeroPlacedEvent += OnHeroPlaced;
        }

        protected void OnDisable()
        {
            UIEventManager.HeroSoldEvent -= OnHeroSold;
            UIEventManager.HeroPlacedEvent -= OnHeroPlaced;
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
