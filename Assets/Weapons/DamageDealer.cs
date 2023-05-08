using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    
    protected float baseDamage = 0.50f;
    protected static float baseAttackCooldown = 0.50f;
    private float attackCooldown = baseAttackCooldown;
    private bool attacking = false;
    private bool canAttack = true;
    
    void OnTriggerEnter2D(Collider2D collision)
    {



    }


    void OnTriggerStay2D(Collider2D collision)
    {
        Pawn pawn = collision.gameObject.GetComponent<Pawn>();

        if (pawn != null)
        {
            if (canAttack && attacking)
            {
                pawn.TakeDamage(baseDamage);
                attacking = false;
            }
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {


    }


    public bool CanAttack()
    {
        return canAttack;
    }

    public bool Attacking()
    {
        return attacking;
    }

    public void Attack()
    {
        attacking = true;
    }


    public abstract void FrameUpdate();

    public void Update()
    {
        if (!canAttack)
        {
            if(attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }
            else
            {
                canAttack = true;
                attackCooldown = baseAttackCooldown;
            }
        }


        FrameUpdate();

    }


}
