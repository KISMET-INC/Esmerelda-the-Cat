using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//****************************************************************
// PLAYER JOYSTICK SCRIPTS
// Moves the character left or right depending on joystick, or
// on screen button press
//****************************************************************
public class PlayerJoystickScript : MonoBehaviour
{
    //Speed and velocity of player
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private float maxVelocity = 4f;

    [SerializeField]
    private Rigidbody2D myBody;
    [SerializeField]
    private Animator anim;

    //Scale of player
    [SerializeField]
    private float scale = 1f;

    private bool moveLeft, moveRight;

    private void Awake()
    {
        //Get and make reference these components on the current object
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //   Scale the  Player
        // - create a vector to manipulate player scale,
        // - set all the parameters to variable set above
        // - transforme the scale of the player
        Vector3 scaleVector;
        scaleVector.x = scale;
        scaleVector.y = scale;
        scaleVector.z = scale;
        transform.localScale = scaleVector;
    }



    //****************************************************************
    // FixedUpdate()
    // Called several times per frame. If bool for move left is
    // true, call MoveLeft() function, if bool for move right is
    // true, call MoveRight() function
    //****************************************************************
    public void Update()
    {
        if (moveLeft)
        {
            MoveLeft();
        }
       else if (moveRight)
        {
            MoveRight();
        }
    }

    //****************************************************************
    // SetMoveLeft()
    // Set true or false to the variables for bool variables
    // moveLeft and moveRight depending on what is passed as a
    // parameter
    //****************************************************************
    public void SetMoveLeft(bool moveLeft)
    {
        this.moveLeft = moveLeft;
        this.moveRight = !moveLeft;       
    }

    //****************************************************************
    // StopMoving()
    // Set moveLeft and moveRight variables to false.
    // Stop the character animation
    //****************************************************************
    public void StopMoving()
    {
        moveLeft = moveRight = false;
        anim.SetBool("Walk", false);
        Debug.Log("Stop");
    }

    //****************************************************************
    // MoveLeft()
    // Move the rigid body left
    //****************************************************************
    void MoveLeft()
    {
        
            //variable to determine direction player should move
            float forceX = 0f;
        float velocity = Mathf.Abs(myBody.velocity.x);

        if (velocity < maxVelocity) 
       
            //variable set for player to move left
            forceX = -speed;

            //set walk animation to play
            anim.SetBool("Walk", true);

                //Create a vector to flip the direction character faces when moving
            Vector3 temp = transform.localScale;
            temp.x = scale;
            transform.localScale = temp;
             


        // Use forceX variable to move the players rigid body left or right appropriately
        myBody.AddForce(new Vector2(forceX, 0));
        Debug.Log("Move Left");

    }
    //****************************************************************
    // MoveRight()
    // Move the rigid body right and flip the animation to face
    // the right way
    //****************************************************************
    void MoveRight()
    {
        //variable to determine direction player should move
        float forceX = 0f;
        float velocity = Mathf.Abs(myBody.velocity.x);

        if (velocity < maxVelocity)
        
            //variable set for player to move left
            forceX = speed;

            //set walk animation to play
            anim.SetBool("Walk", true);

            //Create a vector to flip the direction character faces when moving
            Vector3 temp = transform.localScale;
            temp.x = -scale;
            transform.localScale = temp;
            
        
            // Use forceX variable to move the players rigid body left or right appropriately
        myBody.AddForce(new Vector2(forceX, 0));
        Debug.Log("Move Right");
    }
} // END PLAYER JOYSTICK SCRIPT

