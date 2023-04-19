using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanceVrotController : MonoBehaviour
{

    [SerializeField] Animator _animation;
    [SerializeField] Rigidbody2D MyRigidBody;
    [SerializeField] float moveScale = 200f;
    [SerializeField] SpriteRenderer MySpriteRenderer;
    [SerializeField] Sprite NorthStationary;
    [SerializeField] Sprite EastStationary;
    [SerializeField] Sprite SouthStationary;
    [SerializeField] Sprite WestStationary;
    [SerializeField] GameObject Soul; // Child object that controlls 

    private TextMeshPro DebugOutput;




    //Character control
    private string direction = "east"; // east, south, west, north
    private string prevDirection = "east"; // east, south, west, north

    private bool moving = false;



    // Start is called before the first frame update
    void Start()
    {
        //_animation.Play("CharacterWalkup");
        moving = false;
        DebugOutput = Soul.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAmount = Input.GetAxis("Horizontal");
        float yAmount = Input.GetAxis("Vertical");

        DebugOutput.SetText("Moving: " + moving + "; " + MyRigidBody.velocity.x + "/" + MyRigidBody.velocity.y + "; " + direction);
        ProcessControlls(xAmount, yAmount);

        moving = !((MyRigidBody.velocity.x == 0 && MyRigidBody.velocity.y == 0) && (xAmount == 0 && yAmount == 0));
        prevDirection = direction;

        UpdateMotionState(xAmount, yAmount);

        if(prevDirection != direction || !moving)
        {
            UpdateAnims(xAmount, yAmount);
        }




    }


    private void UpdateMotionState(float xAmount, float yAmount)
    {
        if (xAmount == 0 && yAmount == 0)
        {
            // No XY input

            if (MyRigidBody.velocity.x < 0.005 && MyRigidBody.velocity.y < 0.005)
            {
                // No residual force applied
                moving = false;
            }
            else
            {
                // There is still speed on body
            }
        }
        else
        {
            // XY input present so we are on the move
            moving = true;

            if (Mathf.Abs(xAmount) > Mathf.Abs(yAmount))
            {
                // Horizontal motion is higher than vertical
                direction = (xAmount > 0) ? "east" : "west";
            }
            else
            {
                // Vertical motion is highan than horizontal
                direction = (yAmount > 0) ? "north" : "south";

            }


            // if ()
            //  {

            //  }


        }

    }


    private void UpdateAnims(float xAmount, float yAmount)
    {

        if (!moving)
        {

            // We are fully stationary
            // _animation.Play("CharacterIDLE");
            _animation.enabled = false; 
                switch (direction)
                {
                    case "east":
                    //Debug.Log("Stopped EAST");

                        MySpriteRenderer.sprite = NorthStationary;
                        break;
                    case "west":
                    //Debug.Log("Stopped WEST");

                        MySpriteRenderer.sprite = WestStationary;
                        break;
                    case "south":
                    //Debug.Log("Stopped SOUTH");

                    MySpriteRenderer.sprite = SouthStationary;
                        break;
                    case "north":
                    //Debug.Log("Stopped SOUTH");

                    MySpriteRenderer.sprite = NorthStationary;
                        break;
                }

        }
        else
        {
            _animation.enabled = true;

            switch (direction)
            {
               
                case "east":
                     MySpriteRenderer.flipX = false;

                    _animation.Play("CharacterWalking");
                    break;
                case "west":
                    MySpriteRenderer.flipX = true;
                    _animation.Play("CharacterWalking");
                    break;
                case "south":
                    _animation.Play("CharacterWalkdown");
                    break;
                case "north":
                    _animation.Play("CharacterWalkup");
                    break;
            }

        }


    }


    private void ProcessControlls(float xAmount, float yAmount)
    {



        if (Mathf.Abs(xAmount) > 0 || Mathf.Abs(yAmount) > 0)
        {
            //transform.position += new Vector3(moveScale * xAmount * Time.deltaTime, moveScale * yAmount * Time.deltaTime, 0);
            MyRigidBody.velocity  = (new Vector3((moveScale * xAmount) * Time.deltaTime, (moveScale * yAmount) * Time.deltaTime, 0));
        }

    }



}
