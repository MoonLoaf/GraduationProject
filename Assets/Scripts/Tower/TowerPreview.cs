using UnityEngine;

namespace Tower
{
    /// <summary>
    /// Class to represent the "hovered" tower while it's being placed
    /// </summary>
    public class TowerPreview : MonoBehaviour
    {
        private TowerType _type;
        
        //TODO: this needs functionality to move while touch is still active and then be placed on valid position when touch is deactivated
        //TODO: this then needs to create an instance of Towerbase or something more specific on release
    }
}
