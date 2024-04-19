using System;
using UI;
using UnityEngine;

namespace Tower
{
    public class RangeShaderController : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private GameObject _rangeQuad;

        private void OnEnable()
        {
            UpgradeTab.OnTowerDeselect += SetDisplayRange;
        }

        private void OnDisable()
        {
            UpgradeTab.OnTowerDeselect -= SetDisplayRange;
        }

        public void SetRange(float range)
        {
            float adjustedRange = range * 2.5f;
            _rangeQuad.transform.localScale = new Vector3(adjustedRange, adjustedRange, 1);
            _renderer.material.SetFloat("_Border_Thickness", 0.01f/adjustedRange);
            Debug.Log("Range set");
        }

        public void SetDisplayRange(bool active) => _renderer.enabled = active;
    }
}
