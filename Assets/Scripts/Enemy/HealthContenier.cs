using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthContenier : MonoBehaviour
{
    [SerializeField] private int _health;
    public event UnityAction<int> HelthChanged;
    public event UnityAction Died;
    public void TakeDamage(int value)
    {
        _health -= value;
        if (_health <= 0)
        {
            _health = 0;
            Died?.Invoke();
        }
        HelthChanged?.Invoke(_health);
    }
}
