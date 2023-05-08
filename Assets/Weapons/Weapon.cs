using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageDealer : MonoBehaviour
{
    
    protected float baseDamage = 0.50f;
    protected static float baseAttackCooldown = 0.50f;
    protected string direction = "south"; //default
    protected string prevDirection = "south";

    private float attackCooldown = baseAttackCooldown;
    private bool attacking = false;
    private bool canAttack = true;
    private bool enemyInRange = false;
    private Pawn enemy = null;
    private List<Pawn> enemiesInRange = new List<Pawn>();

    void ProcessAttack(Collider2D collision)
    {

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {

        Pawn pawn = collision.gameObject.GetComponent<Pawn>();
        Debug.Log(pawn);
        if (pawn != null)
        {

            enemyInRange = true;
            enemy = pawn;
//            if (!enemiesInRange.Contains(pawn))
           // {
                //enemiesInRange.Add(pawn);
               // Debug.Log("Added pawn to enemies in range");
            //}
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {

    }


    void OnTriggerExit2D(Collider2D collision)
    {

        Pawn pawn = collision.gameObject.GetComponent<Pawn>();
        Debug.Log(pawn);
        if (pawn != null)
        {
            enemyInRange = false;
            enemy = null;
            //if (!enemiesInRange.Contains(pawn))
            // {
            //enemiesInRange.Remove(pawn);
            // Debug.Log("Removed pawn to enemies in range");
            //}
        }


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
        if (canAttack)
        {
            canAttack = false;
            attacking = true;

            if (enemyInRange)
            {
                enemy.TakeDamage(baseDamage);
            }

        }
    }

    public abstract void Awake();
    public abstract void Start();
    public abstract void FrameUpdate();

    public void RotateWeapon(Transform parent, string newDirection)
    {

        // UGH THIS IS SO MESSY IM SORRY
        if (direction != newDirection)
        {
            prevDirection = direction;
            direction = newDirection;
            
            float rotationDeg = 0.0f;

            switch (prevDirection)
            {

                case "north":

                    switch (direction)
                    {

                        default:
                            rotationDeg = 0.0f;

                            break;

                        case "east":
                            rotationDeg = 90.0f;

                            break;

                        case "south":
                            rotationDeg = 180.0f;


                            break;

                        case "west":
                            rotationDeg = -90.0f;
                            break;

                    }

                    break;

                case "east":
                    switch (direction)
                    {

                        default:
                            rotationDeg = 0.0f;

                            break;

                        case "north":
                            rotationDeg = -90.0f;

                            break;

                        case "south":
                            rotationDeg = 90.0f;


                            break;

                        case "west":
                            rotationDeg = 180.0f;
                            break;

                    }
                    break;

                case "south":
                    switch (direction)
                    {

                        default:
                            rotationDeg = 0.0f;

                            break;

                        case "north":
                            rotationDeg = 180.0f;

                            break;

                        case "east":
                            rotationDeg = -90.0f;


                            break;

                        case "west":
                            rotationDeg = 90.0f;
                            break;

                    }
                    break;

                case "west":
                    switch (direction)
                    {

                        default:
                            rotationDeg = 0.0f;

                            break;

                        case "north":
                            rotationDeg = 90.0f;

                            break;

                        case "east":
                            rotationDeg = 180.0f;


                            break;

                        case "south":
                            rotationDeg = -90.0f;
                            break;

                    }
                    break;

            }


            transform.RotateAround(parent.position, Vector3.back, rotationDeg);

        }

    }
  

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
