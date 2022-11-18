using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "HandAbility", menuName ="Player/Ability/Hand", order = 51)]
public class HandAbility : Ability
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _usefulTime;
    private AttackState _state;
    private Coroutine _coroutine;
    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState attack)
    {
        if (_coroutine != null)
            ResetAbility();
        _state = attack;
        _coroutine = _state.StartCoroutine(Attack(_state));
        _state.AtackCollisionDetected += OnPlayerAttack;
    }
    private IEnumerator Attack(AttackState state)
    {
        float time = _usefulTime;
        while (time > 0)
        {
            _state.Rigidbody.velocity = _state.Rigidbody.velocity.normalized * _attackForce;
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        ResetAbility();
        AbilityEnded?.Invoke();
    }
    private void ResetAbility()
    {
        _state.Rigidbody.velocity = Vector3.zero;
        _state.StopCoroutine(_coroutine);
        _coroutine = null;
        _state.AtackCollisionDetected -= OnPlayerAttack;
    }
    private void OnPlayerAttack(IDamageable damageable)
    {
        if (damageable.ApplyDamage(_state.Rigidbody, _attackForce) == false)
            return;
        _state.Rigidbody.velocity /= 2;
    }
}
