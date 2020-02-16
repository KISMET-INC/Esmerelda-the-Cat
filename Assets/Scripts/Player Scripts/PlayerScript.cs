using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//****************************************************************
// PLAYER SCRIPT CLASS
// Moves the player according to the keyboard or mouse. Starts
// and stops the walk animation. Sets speed and velocity of 
// player.
//****************************************************************
public class PlayerScript : MonoBehaviour
{
        //DECLARE VARIABLES

        //Speed and velocity of player
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private float maxVelocity = 4f;

        //Scale of player
    [SerializeField]
    private float scale = 1f;

        //Attach the players rigidbody so that it can be referenced in this class
    [SerializeField]
    private Rigidbody2D myBody;
    private Animator anim;

    //****************************************************************
    // Awake()
    // Scales the character
    //****************************************************************
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
    // PlayerMoveKeyboard()                                         
    // Recieve data from the keyboard to move the character left    
    // or right and to face the character the appropriate           
    // direction while moving.                                      
    //****************************************************************
    private void PlayerMoveKeyboard()
    {
            //Check if user is ready
        if (GameplayController.instance.ready)
        {   
                //variable to determine direction player should move
            float forceX = 0f;
            float velocity = Mathf.Abs(myBody.velocity.x);

                //Get keys pressed that move character horizontally
                // 0 = not moving / no key pressed
                // -1 = moving left (A)
                // 1 = moving right (D)
            float horizontalKeyPress = Input.GetAxisRaw("Horizontal");

            if (horizontalKeyPress > 0) // 1 (right)
            {
                if (velocity < maxVelocity)
                        //variable set for player to move right
                    forceX = speed;

                    //set walk animation to play 
                anim.SetBool("Walk", true);
                
                    //Create a vector to flip the direction character faces when moving
                Vector3 transformPlayer = transform.localScale;
                transformPlayer.x = -scale;
                transform.localScale = transformPlayer;

            }
            else if (horizontalKeyPress < 0) // -1 (left)
            {

                if (velocity < maxVelocity)
                        //variable set for player to move left
                    forceX = -speed;

                    //set walk animation to play
                anim.SetBool("Walk", true);

                    //Create a vector to flip the direction character faces when moving
                Vector3 temp = transform.localScale;
                temp.x = scale;
                transform.localScale = temp;
            }
            else
            {
                    //Horizontal key press = 0, stop walk animation
                anim.SetBool("Walk", false);
            }
                // Use forceX variable to move the players rigid body left or right appropriately
            myBody.AddForce(new Vector2(forceX, 0));
        }
    }


    //****************************************************************
    // Update()
    // Calls the PlayerMoveKeyboard() function every frame
    // FixedUpdate is only called every 3 or 4 frame (Best when adjusting physics)
    //****************************************************************
    void Update()
    {
        PlayerMoveKeyboard();
    }
}
