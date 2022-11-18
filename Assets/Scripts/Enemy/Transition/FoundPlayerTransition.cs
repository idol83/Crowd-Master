using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundPlayerTransition : EnemyTransition
{
    [SerializeField] private float _findDistance;
    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < _findDistance)
        {
            NeedTransit = true;
        }
    }
}
