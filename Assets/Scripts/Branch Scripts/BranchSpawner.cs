
using UnityEngine;

//****************************************************************
// BRANCH SPAWNER CLASS
// Randomly spawns branches, collectables, and positions the
// player for start
//****************************************************************
public class BranchSpawner : MonoBehaviour
{
        //DECLARE VARIABLES

        //Variable to hold the position of the last branch to trigger spawning of next set of branches
    private float lastBranchPositionY;

        //Link the player game object
    private GameObject player;

        //Variable used to aid in zig-zag spawning
    private float controlX = 0;
    private float minPos = 0.0f;

        //Branch Stretch input Values
    [SerializeField]
    private float branchStretchMin = .07f;
    [SerializeField]
    private float branchStretchMax = 1.05f;
    [SerializeField]
    private int decimalPlacesForStretchValue = 4;

        //Set Bounds for Branch Spawn
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;

        //Mid range to help zig-zag
    [SerializeField]
    private float midX;

        //Transform distance of bad branches
    [SerializeField]
    private float transformBadBranchMin;
    [SerializeField]
    private float transformBadBranchMax;

        //Y distance between branches spawned
    [SerializeField]
    private float distanceBetweenBranches = 4.5f;

        //Y distance above branches that collectables are spawned
    [SerializeField]
    private float distanceAboveBranches = 0.9f;

        //Sprite to swap out
    [SerializeField]
    private Sprite healthyBranch;
   
        //Array of branches
    [SerializeField]
    private GameObject[] branches = null;

        //Array of collectable objects
    [SerializeField]
    private GameObject[] collectables = null;

    //****************************************************************
    // Awake()                                                      
    // Called before first frame renders. Call CreatBranches()      
    // function. Find "Player" game object and link and link to     
    // player variable. Set all collectable objects to inactive.    
    //****************************************************************
    void Awake()
    {
        CreateBranches();
        player = GameObject.Find("Player");

        for (int i = 0; i < collectables.Length; i++)
        {
            collectables[i].SetActive(false);
        }
    }

    //****************************************************************
    // Start()                                                      
    // Called with first frame render. Call PositionPlayer()        
    // function                                                     
    //****************************************************************
    private void Start()
    {
        PositionPlayer();
    }

    //****************************************************************
    // ShuffleObjectsInArray()                                      
    // Iterate through array 'x' number of times and replace        
    // current object index  with another random object index       
    // in the array until the entire array has been iterated through
    //****************************************************************
    void ShuffleObjectsInArray(GameObject[] arrayToShuffle)
    {
        for (int i = 0; i < arrayToShuffle.Length; i++)
        {
            GameObject temp = arrayToShuffle[i];
            int random = Random.Range(i, arrayToShuffle.Length);
            arrayToShuffle[i] = arrayToShuffle[random];
            arrayToShuffle[random] = temp;
        }
    }

    //****************************************************************
    // DontScaleDeadBranch()                                        
    // If the game object passed to it has the tag "badBranch" set  
    // the scale to a stationary value. else leave the scale value  
    // alone to scale other branches.                               
    //****************************************************************
    private float DontScaleDeadBranch(GameObject branch, Vector3 scale) {
       if( branch.tag == "badBranch")
        {
           return scale.x = 1f;
        }
        return scale.x;
    }

    //****************************************************************
    // SpawningBoundsForDeadBranches()                              
    // Limit the spawning bounds for dead branches                  
    //****************************************************************
    private float SpawningBoundsForDeadBranches(GameObject branch, Vector3 transform)
    {
        if (branch.tag == "badBranch")
        {
            return transform.x = Random.Range(transformBadBranchMin,transformBadBranchMax);
        }
        return transform.x;
    }

    //****************************************************************
    // BranchStretch()                                              
    // Create a random scale X value in order to spawn healthy      
    // branches at random widths. AUses Math.Round to limit the     
    // amout of decimal point values for more discrete and easily   
    // noticable differences.                                       
    //****************************************************************
    private float BranchStretch(Vector3 scale)
    {
        scale.x = Random.Range(branchStretchMin, branchStretchMax);
        double scaleTemp = System.Math.Round(scale.x, decimalPlacesForStretchValue);
        scale.x = (float)scaleTemp;
 
        return scale.x;
    }


    //****************************************************************
    // CreateBranches()                                             
    // Shuffle branches array and place in a zig zag pattern and    
    // stretching randomly                                          
    //****************************************************************
    void CreateBranches()
    {
            //Shuffle branches
        ShuffleObjectsInArray(branches);

            //Starting Y position for branches
        float positionY = 5f;

        for (int i = 0; i < branches.Length; i++)
        {
                //Create a vector using the current branches position to have a leaping ground for the next branch to spawn from.
            Vector3 transform = branches[i].transform.position;
            transform.y = positionY;

                //Crete a vector with the scale value of the current branch
            Vector3 scale = branches[i].transform.localScale;

            if (controlX == 0)
            {
                    //Position branch between 0.0 and maxX value (spawn right)
                transform.x = Random.Range(minPos, maxX);  

                controlX = 1;
            }

            else if (controlX == 1)
            {
                    //Position branch between 0.0 and minX value (spawn left)
                transform.x = Random.Range(minPos, minX);    

                controlX = 2;
            }

            else if (controlX == 2)
            {
                    //Position branch between mid value and max value (push to spawn further right)
                transform.x = Random.Range(midX, maxX);
                
                 controlX = 3;
            }

            else if (controlX == 3)
            {
                    //Position branch between NEG mid value and min value (push to spawn further left)
                transform.x = Random.Range(-midX, minX);
        
                controlX = 0;
            }

                //Call the BranchStretch function to create a random stretch value for current branch.
            scale.x = BranchStretch(scale);

                //If the current branch call DontScaleDeadBranch function
            scale.x = DontScaleDeadBranch(branches[i], scale);

                //If branch is dead, limit the spawning bounds to near the center
            transform.x = SpawningBoundsForDeadBranches(branches[i], transform);
                 

                //Reset lastBranchPositionY to the value of every branch in the array until we get to the
                //actual last branch where the value stays.
            lastBranchPositionY = positionY;

                //Take the position of the last branch spawned and move it down the value of the distance chosen between the branches
            positionY -= distanceBetweenBranches;

                //For healthy branches, position them in a zig-zag fashion and stretch them randomly
            branches[i].transform.position = transform;
            branches[i].transform.localScale = scale;
        }

    } //END CREATE BRANCHS


    //****************************************************************
    // PositionPlayer()                                             
    // Find the location of the first good branch and place the     
    // player on top of it                                          
    //****************************************************************
    void PositionPlayer()
    {
        GameObject[] goodBranches = GameObject.FindGameObjectsWithTag("goodBranch");

        //Create a variable to compare Y position values of all good branches in array
        Vector3 goodBranchLocationY = goodBranches[0].transform.position;

        //Iterate to determine the first good branch using a placeholder variable and switching
        for (int i = 1; i < goodBranches.Length; i++)
        {
            if (goodBranchLocationY.y < goodBranches[i].transform.position.y)
            {
                goodBranchLocationY = goodBranches[i].transform.position;
            }

        }
        player.transform.position = goodBranchLocationY + new Vector3(0.0f, 1.0f, 0.0f);

    }


    //****************************************************************
    // SPAWN NEW BRANCHES
    //****************************************************************
    // OnTrigger()                                                  
    // Check if Spawner has passed the lastBranch then shuffle      
    // branches and respawn branches and collectables               
    //****************************************************************
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "goodBranch" || target.tag == "badBranch")
        {
            //Check if trigger is the last branch
            if (target.transform.position.y == lastBranchPositionY)
            {
                ShuffleObjectsInArray(branches);
                ShuffleObjectsInArray(collectables);

                //Get target position
                Vector3 transform = target.transform.position;


                for (int i = 0; i < branches.Length; i++)
                {
                    //Get the scale value of each branch iterated through
                    Vector3 scale = branches[i].transform.localScale;

                    //Check if current branch is inactive 
                    if (!branches[i].activeInHierarchy)
                    {

                        //If inactive, position in a zig zag pattern

                        if (controlX == 0)
                        {
                            //Position branch between 0.0 and maxX value (spawn right)
                            transform.x = Random.Range(minPos, maxX);

                            controlX = 1;
                        }

                        else if (controlX == 1)
                        {
                            //Position branch between 0.0 and minX value (spawn left)
                            transform.x = Random.Range(minPos, minX);

                            controlX = 2;
                        }

                        else if (controlX == 2)
                        {
                            //Position branch between mid value and max value (push to spawn further right)
                            transform.x = Random.Range(midX, maxX);

                            controlX = 3;
                        }

                        else if (controlX == 3)
                        {
                            //Position branch between NEG mid value and min value (push to spawn further left)
                            transform.x = Random.Range(-midX, minX);

                            controlX = 0;
                        }

                        //Call the BranchStretch function to create a random stretch value for current branch.
                        scale.x = BranchStretch(scale);

                        //If the current branch call DontScaleDeadBranch function
                        scale.x = DontScaleDeadBranch(branches[i], scale);

                        //If branch is dead, limit the spawning bounds to near the center
                        transform.x = SpawningBoundsForDeadBranches(branches[i], transform);

                        //Take the position of the last branch spawned and move it down the value of the distance chosen between the branches
                        transform.y -= distanceBetweenBranches;

                        //Reset lastBranchPositionY to the value of every branch in the array until we get to the
                        //actual last branch where the value stays.
                        lastBranchPositionY = transform.y;

                        //For healthy branches, position them in a zig-zag fashion and stretch them randomly
                        branches[i].transform.position = transform;
                        branches[i].transform.localScale = scale;
                        branches[i].SetActive(true);


                        //****************************************************************
                        // SPAWN COLLECTABLES
                        //
                        //****************************************************************

                        int random = Random.Range(0, collectables.Length);
                        // If the current branch is not a bad branch spawn collectables above the branches
                        if (branches[i].tag != "badBranch")
                        {
                            if (!collectables[i].activeInHierarchy)
                            {    
                                //Get position of current branch
                                Vector3 collectableSpawnPosition = branches[i].transform.position;

                                //Add the distance above the branches that you want collectables to spawn to the position value
                                //Hold this value for later
                                collectableSpawnPosition.y += distanceAboveBranches;

                                //If the current collectable is a life check if the user has more than 9 lives
                                if (collectables[i].tag == "life")
                                {
                                    if (PlayerScore.lifeCount < 9)
                                    {
                                        //If player has fewer than 9 lives the spawn a life above this branch and make active
                                        collectables[i].transform.position = collectableSpawnPosition;
                                        collectables[i].SetActive(true);
                                    }

                                } else {

                                    //If not a life collectable, spawn the collectable at the position of the current branch
                                    //plus the distance above branches that was set in temp2
                                    collectables[i].transform.position = collectableSpawnPosition;
                                    collectables[i].SetActive(true);
                                }
                            }
                        }
                   }
                }
            }
        }
    }

}//END BRANCH SPAWNER
