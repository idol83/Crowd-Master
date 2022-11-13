using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class EnemyStateMachine : MonoBehaviour, IDamageable
{
    [SerializeField] private State _firstState;
    [SerializeField] private BrokenState _brokenState;
    [SerializeField] private HealthContenier _healthContenier;
    private State _curentState;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float _minDamage;
    public event UnityAction<EnemyStateMachine> Died;
    public PlayerStateMachine Player { get; private set; }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void OnEnemyDied()
    {
        enabled = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Player = FindObjectOfType<PlayerStateMachine>();
    }
    private void Start()
    {
        _curentState = _firstState;
        _curentState.Enter(_rigidbody, _animator);
    }
    private void Update()
    {
        if (_curentState == null) return;
        State nextState = _curentState.GetNextState();
        if (nextState != null)
        {
            Transit(nextState);
        }
    }
    private void Transit(State nextState)
    {
        if (_curentState != null) _curentState.Exit();
        _curentState = nextState;
        if (_curentState != null)
        {
            _curentState.Enter(_rigidbody, _animator);
        }
    }

    public bool AplyDamage(Rigidbody rigidbody, float damage)
    {
        if (damage > _minDamage && _curentState != _brokenState)
        {
            _healthContenier.TakeDamage((int)damage);
            Transit(_brokenState);
            return true;
        }
        return false;
    }
}
