using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : Pawn
{
    [SerializeField] Animator beetleAnims;
    [SerializeField] SpriteRenderer beetleSprite;
    [SerializeField] CircleCollider2D damageCirlce;

    private string direction = "south";
    private float attackCooldown = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        beetleAnims.Play("BeetleVerticalMove");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEntered2D(Collider2D collider)
    {
        if(collider.tag == "PlayerSoul")
        {

        }


    }

    override protected void OnDamageTaken()
    {
        Debug.LogWarning("Beetle took damage");
    }

    override public void Die()
    {
        Debug.Log("Beetle is mc dead");
    }

}
