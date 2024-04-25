using UI;
using UnityEngine;

namespace Tower
{
    public class RangeShaderController : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private GameObject _rangeQuad;

        private const string ShaderBorderThickness = "_Border_Thickness";
        private readonly int _borderThickness = Shader.PropertyToID(ShaderBorderThickness);

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
            _renderer.material.SetFloat(_borderThickness, 0.01f/adjustedRange);
        }

        public void SetDisplayRange(bool active) => _renderer.enabled = active;
    }
}
