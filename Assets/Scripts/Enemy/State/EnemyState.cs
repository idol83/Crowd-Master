using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] protected EnemyTransition[] _transitions;
    public Rigidbody Rigidbody { get; private set; }
    protected Animator Animator { get; private set; }
    protected PlayerStateMachine Player;
    public void Enter(Rigidbody rigidbody, Animator animator, PlayerStateMachine player)
    {
        if (!enabled)
        {
            Player = player;
            Rigidbody = rigidbody;
            Animator = animator;
            enabled = true;
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(player);
            }
        }
    }
    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }
    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }
        return null;
    }
}
