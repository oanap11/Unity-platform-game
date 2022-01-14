using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 6f, jumpForce = 10f;

    private Rigidbody2D myBody;

    private Vector3 tempPos;

    private PlayerAnimation playerAnim;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        //HandleMovementWithTransform();
        HandlePlayerAnimations();
    }

    private void FixedUpdate()
    {
        HandleMovementWithRigidBody();
    }

    //moving the player from left to right
    void HandleMovementWithTransform()
    {

        tempPos = transform.position;
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            tempPos.x -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            tempPos.x += moveSpeed * Time.deltaTime;
        }

        transform.position = tempPos;

    }

    //moving the player while applying velocity with the RigidBody
    void HandleMovementWithRigidBody()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {

            myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            
        }
        else
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        
    }

    void HandlePlayerAnimations()
    {

        playerAnim.Play_WalkAnimation((int)Mathf.Abs(myBody.velocity.x)); //using the absolute value because velocity can be negative
        
        playerAnim.SetFacingDirection((int)myBody.velocity.x);

    }

} //end
