using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    private State _curentState;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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
}
