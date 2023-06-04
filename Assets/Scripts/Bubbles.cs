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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Lets record our input
    private void OnMouseDown()
    {
        firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        // Screen to world point just changes unity default of pixel coordinates to our world coordinates fo row, column
    }

    private void OnMouseEnter()
    {
        currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (prevTouchPos == firstTouchPos)
        {
            // Initial drag, calculate angle from firstTouchPos to currentTouchPos
            CalculateAngle(firstTouchPos, currentTouchPos);
        }
        else
        {
            // Subsequent drag, calculate angle from prevTouchPos to currentTouchPos
            CalculateAngle(prevTouchPos, currentTouchPos);
        }

    }

    private void OnMouseUp()
    {
        finalTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void CalculateAngle(Vector2 fromPos, Vector2 ToPos)
    {
        currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        swipeAngle = Mathf.Atan2(currentTouchPos.y - firstTouchPos.y, currentTouchPos.x - firstTouchPos.x);
    }
}
