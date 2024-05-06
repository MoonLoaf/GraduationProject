using Tower.Hero;
using UnityEngine;
using Utility;

namespace UI
{
    [DefaultExecutionOrder(-1)]
    public class UIEventManager : GenericSingletonDOL<UIEventManager>
    {
        public delegate void UIEventHandler();

        public delegate void HeroEventHandler(Hero hero);
        public event UIEventHandler TowerPlacedEvent;
        public UIEventHandler OnSettingsPressed;
        public UIEventHandler OnGameContinue;
        public HeroEventHandler HeroSoldEvent;
        public HeroEventHandler HeroPlacedEvent;
        public bool IsPreviewActive { get; private set; } = false;

        public void NotifyTowerPlaced()
        {
            IsPreviewActive = false;
            TowerPlacedEvent?.Invoke();
        }
        
        public void StartTowerPreview()
        {
            IsPreviewActive = true;
        }
    }
}
