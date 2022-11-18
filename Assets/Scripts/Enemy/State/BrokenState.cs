using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class BrokenState : EnemyState
{
    [SerializeField] private float _fallDistance;
    public event UnityAction Died;
    public void ApplyDamage(Rigidbody attachedBody, float damage)
    {
        Animator.SetTrigger("GetHit");
        Vector3 direction = (transform.position - attachedBody.position);
        direction.y = 0;
        Rigidbody.AddForce(direction.normalized * damage, ForceMode.Impulse); 
        Debug.Log("ghbdtn");
    }
    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if (!Physics.Raycast(ray, _fallDistance))
        {
            Rigidbody.constraints = RigidbodyConstraints.None;
            Died?.Invoke();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(enabled) return;
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(Rigidbody, Rigidbody.velocity.magnitude);
        }
    }
}
