using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private EnemyType _type;
    private int _currentHealth;
    private float _moveSpeed;
    
    void Start()
    {
        //These should be set, the rest won't get used as much and can be accessed from "this._type.Property;"
        _currentHealth = _type.MaxHP;
        _moveSpeed = _type.MovementSpeed;
    }

    void Update()
    {
        
    }

    public EnemyType GetEnemyType()
    {
        return _type;
    }

    public void SetEnemyType(EnemyType newType)
    {
        if(_type != null){return;}
        _type = newType;
    }
}
