using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMortal
{
    public void TakeDamage(float damage);
    public void Die();
}
