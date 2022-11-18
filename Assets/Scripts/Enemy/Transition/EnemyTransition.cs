using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;
    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }
    protected PlayerStateMachine Player { get; private set; }
    public void Init(PlayerStateMachine player)
    {
        Player = player;
    }
    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }
}
