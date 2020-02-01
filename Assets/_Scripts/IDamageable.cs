using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void ApplyDamage(Hitbox hitbox, AttackData attackData);
}
