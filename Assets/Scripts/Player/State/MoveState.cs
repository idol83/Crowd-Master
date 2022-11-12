using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerState
{
    [SerializeField] private PLayerImput _pLayerImput;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRatio;
    [SerializeField] private StaminAccumulator _staminAccumulator;
    private void OnEnable()
    {
        _pLayerImput.DirectionChanged += OnDirectionCanged;
        _staminAccumulator.StaminaAccumulate();
        Animator.SetFloat("Run", 1);
    }
    private void OnDisable()
    {
        _pLayerImput.DirectionChanged -= OnDirectionCanged;
        Animator.SetFloat("Run", 0);
    }
    private void OnDirectionCanged(Vector2 direction)
    {
        Rigidbody.velocity = new Vector3(direction.x, 0,direction.y)* _speedRatio;
        if (Rigidbody.velocity.magnitude > _speed)
            Rigidbody.velocity *= _speed / Rigidbody.velocity.magnitude;
        if (Rigidbody.velocity.magnitude != 0)
            Rigidbody.MoveRotation(Quaternion.LookRotation(Rigidbody.velocity, Vector3.up));
    }
}
