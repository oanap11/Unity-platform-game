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

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform groundCheckPos;

    private BoxCollider2D boxCol2D;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        boxCol2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //HandleMovementWithTransform();
        HandlePlayerAnimations();
        HandleJumping();
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

    //moving the player while adding velocity with the RigidBody
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

        playerAnim.Play_JumpAnimation(!IsGrounded()); //the animation plays when the player is not on the ground

    }

    //checking if the player is on the ground
    bool IsGrounded()
    {
        //casting a box from the center of the game object
        // size, angle, direction(down), box length, checking for collisions
        return Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);

    }

    void HandleJumping()
    {

        if (Input.GetButtonDown(TagManager.JUMP_BUTTON))
        {
            if (IsGrounded())
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
            }
        }
    }

} //end
