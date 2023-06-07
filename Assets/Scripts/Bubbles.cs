using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbles : MonoBehaviour
{

    private Vector2 firstTouchPos;
    private Vector2 finalTouchPos;
    private Vector2 currentTouchPos;
    private Vector2 prevTouchPos;
    public float swipeAngle = 0;
    private Board board;
    //private bool isMousePressed = false; // boolean flag to check if moue is pressed (we dont want bubbles bursting randomly by hovering the mouse
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>(); // find the Board script in the scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Lets record our input
    private void OnMouseDown()
    {
        Debug.Log("Mouse down on bubble!");
        //isMousePressed = true;
        firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Screen to world point just changes unity default of pixel coordinates to our world coordinates fo row, column


        // check what game state ur in
        // if board reffil state -----> wait (no interaction)
        // else if in interaction state(mouse up timer is running) ------> reset timer ----> continue interaction state
        // if board filled state -----> begin interaction state


        //check for color correctness
        // if right bubble -----> break ----> initiate haptic----> generate points
        // else ------> end interaction state --------> enter board refill state
        board.BreakBubble(firstTouchPos.x, firstTouchPos.y);

        // store the first postion as the prev postion to calculate the angle



        prevTouchPos = firstTouchPos;

        // NESTING ONMOUSE ENTER for now
            // NESTING ONMOUSEENTER
        OnMouseEnter();

        

    }
 
    void OnMouseEnter() // for when player drags to burst bubbles in combo
    {
        if (Input.GetMouseButton(0))
        {
            currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // check angle
            // if angle = 0, 90, 180, 270 --------> go to color check and
            // if right bubble -----> break ----> initiate haptic----> generate points
            // else ------> end interaction state --------> enter board refill state
            // else --------> end interaction state --------> enter board refill state

            //NOTE: for color check, if true points increase for longer drag. (need a counter for nuber of bubbles burst in combo)
            board.BreakBubble(currentTouchPos.x, currentTouchPos.y);





            // update the current touch pos to be the prev touch pos so the angle is calculated for the next drag
            prevTouchPos = currentTouchPos;
        }
    }



    private void OnMouseUp()
    {
        //isMousePressed = false;
        // store earned points in counter 

        // begin hidden timer
        // if timer ends -----> end interaction state --------> enter board refill state
        // if mouse down before timer ends ------> reset timer ////// NOTE: might need to make a seperate func as timer

    }

    void CalculateAngle(Vector2 fromPos, Vector2 ToPos)
    {
        currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        swipeAngle = Mathf.Atan2(currentTouchPos.y - firstTouchPos.y, currentTouchPos.x - firstTouchPos.x);
    }
}



