using UnityEngine;

namespace Tower
{
    public class RangeShaderController : MonoBehaviour
    {
        public Material towerMaterial;

        public void SetRange(float range)
        {
            if (towerMaterial != null)
            {
                towerMaterial.SetFloat("_Range", range);
            }
        }
    }
}
