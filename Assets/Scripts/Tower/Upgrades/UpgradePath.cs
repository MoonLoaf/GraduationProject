using System;
using UnityEngine;

namespace Tower.Upgrades
{
    [Serializable]
    public class UpgradePath
    {
        public TowerUpgrade[] Upgrades;
        private int _progressIndex = 0;
        private bool _isLocked = false;
        private bool _isActive;
        public int ProgressIndex
        {
            get => _progressIndex;
            set
            {
                _progressIndex = Mathf.Clamp(value, 0, 3);
                _isActive = true;
            } 
        }

        public bool IsActive => _isActive;
        
        public bool IsLocked
        {
            get => _isLocked;
            set => _isLocked = value;
        }
    }
}
