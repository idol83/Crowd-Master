using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool AplyDamage(Rigidbody rigidbody, float damage);
}
