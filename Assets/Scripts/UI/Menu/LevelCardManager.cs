using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Menu
{
    public class LevelCardManager : MonoBehaviour
    {
        [SerializeField] private List<LevelInfo> _levelScenes;
        [SerializeField] private GameObject _levelCardPrefab;
        [SerializeField] private float _offset;
        [SerializeField] private Transform _parent;
        private int _levelIndex;
        public delegate void LevelToLoadChangedHandler(string newLevel);
        public delegate void LevelIndexChangedHandler(int change);
        [SerializeField] private float _transitionDuration = 0.5f;
        private bool _isMoving = false;

        public static LevelIndexChangedHandler OnLevelIndexChanged;
        public static LevelToLoadChangedHandler OnLevelToLoadChanged;
        
        private void Start()
        {
            for (int i = 0; i < _levelScenes.Count; i++)
            {
                LevelInfo scene = _levelScenes[i];
                GameObject cardObj = Instantiate(_levelCardPrefab, _parent);

                Vector3 newPos = cardObj.transform.position + new Vector3(_offset * i, 0, 0);
                cardObj.transform.position = newPos;
                
                LevelCard levelCard = cardObj.GetComponent<LevelCard>();
                levelCard.Initialize(scene.SceneSprite, scene.LevelDisplayName);
            }
            OnLevelToLoadChanged?.Invoke(_levelScenes[_levelIndex].SceneName);
        }

        private void OnEnable()
        {
            OnLevelIndexChanged += SetLevelIndex;
        }

        private void OnDisable()
        {
            OnLevelIndexChanged -= SetLevelIndex;
        }

        private void SetLevelIndex(int change)
        {
            int newIndex = Mathf.Clamp(_levelIndex + change, 0, _levelScenes.Count - 1);
    
            if (newIndex != _levelIndex && !_isMoving)
            {
                _levelIndex = newIndex;
                OnLevelToLoadChanged?.Invoke(_levelScenes[_levelIndex].SceneName);
        
                Vector3 targetPosition = _parent.transform.position + new Vector3(-_offset * change, 0, 0);
        
                StartCoroutine(MoveLevelCards(targetPosition));
            }
        }
        
        private IEnumerator MoveLevelCards(Vector3 targetPosition)
        {
            _isMoving = true;

            Vector3 initialPosition = _parent.transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < _transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / _transitionDuration);
                _parent.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
                yield return null;
            }

            _parent.transform.position = targetPosition;
            _isMoving = false;
        }
    }
}
