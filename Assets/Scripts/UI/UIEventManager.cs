using Tower.Hero;
using Utility;

namespace UI
{
    public class UIEventManager : GenericSingletonDOL<UIEventManager>
    {
        public delegate void UIEventHandler();

        public delegate void HeroEventHandler(Hero hero);
        public event UIEventHandler TowerPlacedEvent;
        public static UIEventHandler OnSettingsPressed;
        public static UIEventHandler OnGameContinue;
        public static HeroEventHandler HeroSoldEvent;
        public static HeroEventHandler HeroPlacedEvent;
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
