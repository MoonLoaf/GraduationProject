using System;
using Tower.Hero;
using UnityEngine;
using Utility;

namespace UI
{
    [DefaultExecutionOrder(-1)]
    public class UIEventManager : GenericSingletonDOL<UIEventManager>
    {
        public delegate void HeroEventHandler(Hero hero);

        public event Action TowerPlacedEvent;
        public Action OnSettingsPressed;
        public Action OnGameContinue;
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
