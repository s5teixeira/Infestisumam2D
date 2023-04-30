using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Pawn : MonoBehaviour, IMortal
{

    private float health = 1.0f; // Default health is 1.0
    private float armor = 0.0f; // Armor less by default
    private bool isDead = false;


    public void TakeDamage(float damage)
    {
        float remainingDamage = damage; 
        if(armor > 0.0f)
        {
            remainingDamage = Math.Abs(armor - damage);
        }


        health -= remainingDamage;

        if(health <= 0.0) {
            isDead = true;
            Die();
        }

    }

    public abstract void Die(); // Subclass with implement this

}
