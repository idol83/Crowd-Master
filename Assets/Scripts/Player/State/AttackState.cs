using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : State
{
    [SerializeField] private StaminAccumulator _staminAccumulator;
    private Ability _currentAbility;
    public event UnityAction<IDamageable> AtackCollisionDetected;
    public event UnityAction AbilityEnded;
    private void OnEnable()
    {
        Animator.SetTrigger("Attack");
        _currentAbility = _staminAccumulator.GetAbility();
        _currentAbility.AbilityEnded += OnAbilityEnded;
        _currentAbility.UseAbility(this);
    }
    private void OnDisable()
    {
        _currentAbility.AbilityEnded -= OnAbilityEnded;
    }
    private void OnAbilityEnded()
    {
        AbilityEnded?.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
            AtackCollisionDetected?.Invoke(damageable);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            AtackCollisionDetected?.Invoke(damageable);
    }
}
