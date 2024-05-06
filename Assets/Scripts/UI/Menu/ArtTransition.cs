using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Menu
{
    public class ArtTransition : MonoBehaviour
    {
        [SerializeField] private float _transitionTime;
        private Vector3 _startPos;
        private Image _imageRef;

        private void Awake()
        {
            _imageRef = GetComponent<Image>(); 
        }

        private void Start()
        {
            SceneLoader.OnSceneLoaded += OnSceneLoaded;
            _startPos = _imageRef.rectTransform.anchoredPosition;
        }

        private void OnSceneLoaded()
        {
            StartCoroutine(Transition());
            SceneLoader.OnSceneLoaded -= OnSceneLoaded;
        }

        private IEnumerator Transition()
        {
            Vector3 targetPos = _startPos + Vector3.up * 350;
            float elapsedTime = 0f;
            
            Debug.Log(_startPos);
            Debug.Log(targetPos);
            
            while (elapsedTime < _transitionTime)
            {
                float t = elapsedTime / _transitionTime;
                _imageRef.rectTransform.anchoredPosition = Vector3.Lerp(_startPos, targetPos, t);
                
                elapsedTime += Time.deltaTime;
                Debug.Log(elapsedTime);
                Debug.Log(t);
                yield return null;
            }

            _imageRef.rectTransform.anchoredPosition = targetPos;
        }
    }
}
