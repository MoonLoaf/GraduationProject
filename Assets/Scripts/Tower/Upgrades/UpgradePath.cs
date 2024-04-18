using System;
using UnityEngine;

namespace Tower.Upgrades
{
    [Serializable]
    public class UpgradePath
    {
        public TowerUpgrade[] Upgrades;
        public int _progressIndex = 0;
        public bool IsLocked = false;
        public int ProgressIndex
        {
            get => _progressIndex;
            set => _progressIndex = Mathf.Clamp(value, 0, 3);
        }
    }
}
