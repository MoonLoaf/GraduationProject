using Utility;

namespace UI
{
    public class UIEventManager : GenericSingleton<UIEventManager>
    {
        public delegate void UIEventHandler();
        public event UIEventHandler TowerPlacedEvent;
        public static UIEventHandler HeroSoldEvent;
        public static UIEventHandler HeroPlacedEvent;
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
