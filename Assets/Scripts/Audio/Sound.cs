using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Sound")]
    public class Sound : ScriptableObject
    {
        public AudioClip Clip;
        [Space] [SerializeField] private SoundType.Type _type;
        [HideInInspector] public AudioSource Source;
        [Space] [SerializeField] private bool _repeatingWithTimer;
        [SerializeField] private float _timer;
        [SerializeField] private bool _loop;

        [Space] [SerializeField] [Range(0f, 100f)]
        private float _volume = 100;

        [SerializeField] [Range(0.1f, 3f)] private float _pitch = 1;
        public float Volume => _volume / 100;
        public float Pitch => _pitch;
        public bool Loop => _loop;
        public bool RepeatingWithTimer => _repeatingWithTimer;
        public float Timer => _timer;
        public SoundType.Type Type => _type;
    }
}