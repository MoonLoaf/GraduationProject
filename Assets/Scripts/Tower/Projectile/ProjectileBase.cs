using System;
using UnityEngine;

namespace Tower.Projectile
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private ProjectileType _type;
        public ProjectileType Type => _type;

        private Vector3 _direction;
        private bool _shouldMove = false;

        private SpriteRenderer _renderer;
        
        private void Awake()
        {
            gameObject.AddComponent<CircleCollider2D>();
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        public void Initialize(ProjectileType type, Vector3 dir)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _direction = dir;
            _shouldMove = true;
        }

        public void SetType(ProjectileType type)
        {
            if(_type != null){return;}
            _type = type;
        }

        private void Update()
        {
            if(!_shouldMove){return;}
            
            Vector3 movement = _direction * (_type.MoveSpeed * Time.deltaTime);

            transform.position += movement;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //TODO: Damage logic
        }
    }
}
