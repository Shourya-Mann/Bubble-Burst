using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interhaptics.Core;


public class Bubbles : MonoBehaviour
{

    private Vector2 firstTouchPos;
    private Vector2 finalTouchPos;
    private Vector2 currentTouchPos;
    private Vector2 prevTouchPos;
    private RaycastHit2D hit; // hit1 is the variable to store what bubble was clicked upon
    //private RaycastHit2D hit; // hit is the variable to store what bubble is the clicked mouse dragged over
    public float swipeAngle = 0;
    bool canBreakBubble;
    //private bool isMousePressed = false; // boolean flag to check if moue is pressed (we dont want bubbles bursting randomly by hovering the mouse
    
    private Board board; // calling the board script
    public ColorChange colorChangeScript;// calling the Color Change Script
    public List<ColorChange> colorChangeScripts;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>(); // find the Board script in the scene
        //olorChangeScript = GetComponent<ColorChange>();
        colorChangeScripts = new List<ColorChange>();

        // Find all GameObjects with the ColorChange script and add their references to the list
        ColorChange[] colorChangeObjects = FindObjectsOfType<ColorChange>();
        foreach (ColorChange colorChangeObject in colorChangeObjects)
        {
            colorChangeScripts.Add(colorChangeObject);
        }
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

        // raycast to figure out which object was clicked

        hit = Physics2D.Raycast(firstTouchPos, Vector2.zero);

        // check what game state ur in
        // if board reffil state -----> wait (no interaction)
        // else if in interaction state(mouse up timer is running) ------> reset timer ----> continue interaction state
        // if board filled state -----> begin interaction state


        //check for color correctness
       
        foreach (ColorChange colorChangeScript in colorChangeScripts)// cause multiple bubbles with colorChange script attached
        {
            if (colorChangeScript.ColorCheck(hit.collider.gameObject))
            {
                canBreakBubble = true;
                break;
            }
        }


        // if right bubble -----> break ----> initiate haptic----> generate points
        // else ------> end interaction state --------> enter board refill state
        if (canBreakBubble)
        {
            board.BreakBubble(firstTouchPos.x, firstTouchPos.y);
            

        }
        else
        {
            Debug.Log("Bubble cannot be broken!");
        }
        // store the first postion as the prev postion to calculate the angle



        prevTouchPos = firstTouchPos;

        // NESTING ONMOUSE ENTER for now doing so creates a null reference error couse the hit value overwrites initially. 
        //OnMouseEnter();

        

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

            // board.BreakBubble(currentTouchPos.x, currentTouchPos.y);

            //check for color correctness
            hit = Physics2D.Raycast(currentTouchPos, Vector2.zero);

            foreach (ColorChange colorChangeScript in colorChangeScripts)// cause multiple bubbles with colorChange script attached
            {
                if (colorChangeScript.ColorCheck(hit.collider.gameObject))
                {
                    canBreakBubble = true;
                    break;
                }
            }

            if (canBreakBubble)
            {
                board.BreakBubble(currentTouchPos.x, currentTouchPos.y);

            }
            else
            {
                Debug.Log("Bubble cannot be broken!");
            }
            //board.BreakBubble(currentTouchPos.x, currentTouchPos.y);
                //OnMouseEnter();
            
           




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
        //currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //swipeAngle = Mathf.Atan2(currentTouchPos.y - firstTouchPos.y, currentTouchPos.x - firstTouchPos.x);
    }


}



