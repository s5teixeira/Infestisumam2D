using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fister : DamageDealer
{
    public override void Awake()
    {

    }

    public override void Start()
    {

    }


    public override void FrameUpdate()
    {
        if (CanAttack())
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            //Debug.Log("REDDD");
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.blue;

        }


    }
}
