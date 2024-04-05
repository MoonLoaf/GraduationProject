using Utility;

namespace UI
{
    public class UIEventManager : GenericSingleton<UIEventManager>
    {
        public delegate void UIEventHandler();
        public event UIEventHandler TowerPlacedEvent;
        
        private bool _previewActive = false;

        public bool IsPreviewActive => _previewActive;

        public void NotifyTowerPlaced()
        {
            _previewActive = false;
            TowerPlacedEvent?.Invoke();
        }
        
        public void StartTowerPreview()
        {
            _previewActive = true;
        }
    }
}
