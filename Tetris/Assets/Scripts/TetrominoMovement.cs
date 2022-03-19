using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
    public GameObject tetromino; //the tetromino that is currently on screen
    public float tetrominoFallSpeedDefault = 1f;  //how often the current tetromino falls by 1 (default)
    public float tetrominoFallSpeedFast = 0.1f;  //how often the current tetromino falls by 1 (soft drop)
    public float tetrominoVerticalMovementTimer = 0f;  //timer which is increased every frame
    float DASTimer = 0f;  //timer which is increased every frame
    public float DAS = 0.5f;  //Delayed Auto Shift - the delay between the player holding left or right, to when the current tetromino starts falling at the ARR
    float ARRTimer = 0f;  //timer which is increased every frame
    public float ARR = 0.2f;  //Auto Repeat Rate - how often the current tetromino moves left or right when the player is holding the respective key
    bool leftARR = false;  //tells us whether the current tetromino should be moving left at the ARR
    bool rightARR = false;  //tells us whether the current tetromino should be moving right at the ARR

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start() //Start is called once when the script is first launched
    {

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void FixedUpdate() //FixedUpdate is called every 0.02 seconds
    {
        tetrominoVerticalMovementTimer += 0.02f; //Timer used to move the tetrominos down at certain speeds
        if (leftARR == true) //if the player has been holding left for more than one frame
        {
            DASTimer += 0.02f; //increase DASTimer by 0.02 for every frame the player is holding left
        }
        if (rightARR == true) //if the player has been holding right for more than one frame
        {
            DASTimer += 0.02f; //increase DASTimer by 0.02 for every frame the player is holding right
        }
        if (DASTimer >= DAS)  //if the player has been holding left for as long their DAS time
        {
            ARRTimer += 0.02f;  //increase ARRTimer by 0.02 for ever frame
        }
        if (DASTimer >= DAS)  //if the player has been holding right for as long their DAS time
        {
            ARRTimer += 0.02f;  //increase ARRTimer by 0.02 for ever frame
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Update()  //called once every frame
    {
        //print(FindObjectOfType<Scoring>().score);

        //Vertical Movement
        if (Input.GetAxisRaw("Vertical") == -1) //if the player presses down
        {
            //this makes the tetromino soft drop (speed can be modified by changingt the tetrominoFallSpeedFast variable)
            if (tetrominoVerticalMovementTimer >= tetrominoFallSpeedFast)
            {
                tetrominoVerticalMovementTimer = 0f;  //reset the timer for vertical movement
                moveTetromino("down", true);  //move the current tetromino down by 1
            }
        }
        //This makes the tetromino drop at the default speed (can be modified by changing the tetrominoFallSpeedDefault variable)
        else if (tetrominoVerticalMovementTimer >= tetrominoFallSpeedDefault)
        {
            tetrominoVerticalMovementTimer = 0f;  //reset the timer for vertical movement
            moveTetromino("down", false);  //move the current tetromino down by 1
        }

        if (Input.GetKeyDown(KeyCode.Space))  //if the player presses the slam keybind
        {
            slamTetromino();  //call the slam tetromino function
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Horizontal Movement
        if (Input.GetAxisRaw("Horizontal") == -1) //if the player presses left
        {
            if (leftARR == false) //if this is the first frame the player is pressing left
            {
                moveTetromino("left", false); //move the current tetromino left by 1
                leftARR = true; //set leftARR to true so that the program now know the player is pressing left
            }
            if (ARRTimer >= ARR)
            {
                moveTetromino("left", false);  //move the current tetromino left by 1
                ARRTimer = 0f;  //reset the ARR Timer
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == 1) //if the player presses right
        {
            if (rightARR == false) //if this is the first frame the player is pressing right
            {
                moveTetromino("right", false); //move the current tetromino left by 1
                rightARR = true; //set leftARR to true so that the program now know the player is pressing right
            }
            if (ARRTimer >= ARR)
            {
                moveTetromino("right", false);  //move the current tetromino right by 1
                ARRTimer = 0f;  //reset the ARR Timer
            }
        }
        else  //if the player is no longer holding left or right
        {
            //reset all of the variables used above
            leftARR = false;
            rightARR = false;
            DASTimer = 0f;
            ARRTimer = 0f;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Rotational Movement
        if (Input.GetKeyDown(KeyCode.E)) //if the player presses their keybind to rotate clockwise
        {
            rotateTetromino("Clockwise");  //call the rotate tetromino procedure, with a parameter specifying a clockwise rotation
        }
        else if (Input.GetKeyDown(KeyCode.W)) //if the player presses their keybind to rotate anti-clockwise
        {
            rotateTetromino("AClockwise");  //call the rotate tetromino procedure, with a parameter specifying an anti-clockwise rotation
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Holding
        if (Input.GetKeyDown(KeyCode.LeftShift))  //if the player presses the hold keybind
            FindObjectOfType<Holding>().holdTetromino();  //call the hold tetromino procedure
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void moveTetromino(string direction, bool isSoftDrop)  //moveTetromino allows us to move the current tetromino in a specified direction by 1
    {
        switch (direction)
        {
            case "left":
                tetromino.transform.position -= new Vector3(1, 0, 0);
                if (!FindObjectOfType<Board>().isTetrominoValid(tetromino.transform))
                    tetromino.transform.position += new Vector3(1, 0, 0);
                FindObjectOfType<GhostMovement>().updateGhost();
                break;
            case "right":
                tetromino.transform.position += new Vector3(1, 0, 0);
                if (!FindObjectOfType<Board>().isTetrominoValid(tetromino.transform))
                    tetromino.transform.position -= new Vector3(1, 0, 0);
                FindObjectOfType<GhostMovement>().updateGhost();
                break;
            case "down":
                tetromino.transform.position -= new Vector3(0, 1, 0);
                if (isSoftDrop)
                {
                    FindObjectOfType<Scoring>().score += 1;
                }
                if (!FindObjectOfType<Board>().isTetrominoValid(tetromino.transform))
                {
                    if(isSoftDrop)
                        FindObjectOfType<Scoring>().score += 1;
                    tetromino.transform.position += new Vector3(0, 1, 0);
                    placeTetromino();
                }
                FindObjectOfType<GhostMovement>().updateGhost();
                break;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void rotateTetromino(string direction)  //rotateTetromino allows us to rotate the current tetromino in a specified direction
    {
        if (direction == "Clockwise")
        {
            tetromino.transform.Rotate(new Vector3(0, 0, -90));
            if (!FindObjectOfType<Board>().isTetrominoValid(tetromino.transform))
                tetromino.transform.Rotate(new Vector3(0, 0, 90));
            FindObjectOfType<GhostMovement>().updateGhost();
        }
        else if (direction == "AClockwise")
        {
            tetromino.transform.Rotate(new Vector3(0, 0, 90));
            if (!FindObjectOfType<Board>().isTetrominoValid(tetromino.transform))
                tetromino.transform.Rotate(new Vector3(0, 0, -90));
            FindObjectOfType<GhostMovement>().updateGhost();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void slamTetromino()  //"slams" the tetromino, meaning it instantly places it as far down as it can go
    {
        while (FindObjectOfType<Board>().isTetrominoValid(tetromino.transform))  //while the tetromino is in a vaild position
        {
            tetromino.transform.position -= new Vector3(0, 1, 0);  //move the tetromino down by 1
            FindObjectOfType<Scoring>().score += 2;
        }
        FindObjectOfType<Scoring>().score += 2;
        tetromino.transform.position += new Vector3(0, 1, 0);  //move it up by 1, since the while loop will always do 1 extra
        placeTetromino();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void placeTetromino()
    {
        FindObjectOfType<GhostMovement>().destroyGhost();
        FindObjectOfType<NextTetromino>().destroyNext();
        if(!FindObjectOfType<Holding>().isHeld)
            FindObjectOfType<Scoring>().tetrominoIndex = FindObjectOfType<TetrominoSpawning>().tempCurrentTetrominoIndex;
        //FindObjectOfType<TetrominoSpawning>().spawnTetromino();
        FindObjectOfType<Board>().addTetrominoToBoard(tetromino.transform);
        
        FindObjectOfType<Board>().lineClearing();
        //FindObjectOfType<GhostMovement>().updateGhost();
        FindObjectOfType<TetrominoSpawning>().spawnTetromino();
        FindObjectOfType<GhostMovement>().updateGhost();
        FindObjectOfType<Holding>().isHeld = false;
        this.enabled = false;
    }
}
