using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class ArtTransition : MonoBehaviour
    {
        [SerializeField] private float _transitionTime;
        [SerializeField] private Image _imageRef;
        
        void Start()
        {
            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            Vector3 startPos = _imageRef.transform.position;
            Vector3 targetPos = startPos + Vector3.up * 100;
            
            float elapsedTime = 0f;

            while (elapsedTime < _transitionTime)
            {
                float t = elapsedTime / _transitionTime;
                _imageRef.transform.position = Vector3.Lerp(startPos, targetPos, t);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _imageRef.transform.position = targetPos;
        }
    }
}
