using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Pawn : MonoBehaviour, IMortal
{

    protected float health = 1.0f; // Default health is 1.0
    protected float armor = 0.0f; // Armor less by default
    protected bool isDead = false;


    public void TakeDamage(float damage)
    {
        // I want single damage taking rule for everyone
        Debug.Log("Took damage of " + damage);
        float remainingDamage = damage; 
        if(armor > 0.0f)
        {
            remainingDamage = Math.Abs(armor - damage);
        }


        health -= remainingDamage;

        OnDamageTaken();
        if(health <= 0.0) {
            isDead = true;
            Die();
        }
        Debug.Log("Took damage of " + damage + ", my health right now is " + health + " and I am " + (isDead ? "dead": "alive"));

    }

    protected abstract void OnDamageTaken(); // Called every time damage is taken; Could be used to update UI or update model 
    public abstract void Die(); // Subclass with implement this

}
