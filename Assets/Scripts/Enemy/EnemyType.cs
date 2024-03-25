using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "EnemyType")]
public class EnemyType : ScriptableObject
{
    [Header("Enemy Stats")]
    [SerializeField] private int _maxHP;
    [SerializeField] private int _damage;
    [SerializeField] private float _movementSpeed;
    
    [Header("Enemy Flags and Modifiers")]
    [SerializeField] private bool isMetal;
    // TODO: Add more modifiers
    
    public int MaxHP => _maxHP;
    public float MovementSpeed => _movementSpeed;
    public int Damage => _damage;
    public bool IsMetal => isMetal;
}
