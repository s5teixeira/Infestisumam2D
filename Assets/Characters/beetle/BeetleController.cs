using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeetleController : Pawn
{
    [SerializeField] Animator beetleAnims;
    [SerializeField] SpriteRenderer beetleSprite;
    [SerializeField] NavMeshAgent agent;


    private string direction = "south";
    private float attackCooldown = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        beetleAnims.Play("BeetleVerticalMove");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isDead)
        {
            Pawn pawn = collision.gameObject.GetComponent<Pawn>();
            pawn.TakeDamage(0.5f);
            agent.destination = pawn.transform.position;

        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
       Debug.Log(collision.gameObject.tag);

    }



    void OnTriggerExit2D(Collider2D collision)
    {


    }


    override protected void OnDamageTaken()
    {
        Debug.LogWarning("Beetle took damage");
    }

    override public void Die()
    {
        Debug.Log("Beetle is mc dead");

        beetleAnims.enabled = false;
        GetComponent<SpriteRenderer>().color = Color.red;

    }

}
