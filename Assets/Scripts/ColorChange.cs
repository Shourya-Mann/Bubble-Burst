using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    // call the Bubbles script 
    //public Bubbles bubbleScript;
    // setting the gameobject to see the large bubble that changes color
    public GameObject directorBubble;
    public GameObject[] coloredBubbles;
    private GameObject currentColorBubble;
    private GameObject decider;
    private Vector2 pos;
    int randomColor;

    public Bubbles bubblesScript; // Reference to the Bubbles script


    // Start is called before the first frame update
    void Start()
    {
        // Setting up the decider bubble
        pos = new Vector2(2, 8.5f);
        decider = Instantiate(directorBubble, pos, Quaternion.identity);
        decider.transform.parent = transform;
        InvokeRepeating("ChangeColor", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void ChangeColor()
    {

        //setting up the decider bubble
        Vector2 pos = new Vector2(2, 8.5f);
        GameObject decider = Instantiate(directorBubble, pos, Quaternion.identity);
        decider.transform.parent = transform;
        //deactivate the current color bubble, if any 

        if (currentColorBubble != null)
        {
            currentColorBubble = null;
            
        }
        


        //randomly select a new colored bubble
        // dowhile loop to prevent the same color repeating
        
        //int prevColor = -1;
        if (coloredBubbles.Length == 1)
        {
            randomColor = 0;
        }
        else
        {
            do
            {
                randomColor = Random.Range(0, coloredBubbles.Length);
            } while (coloredBubbles[randomColor] == currentColorBubble);
        }

        

        //setting the colored bubbles to be the children of the big decider bubble
        currentColorBubble = coloredBubbles[randomColor];
        GameObject bubble = Instantiate(coloredBubbles[randomColor], pos, Quaternion.identity);
        bubble.transform.parent = decider.transform;
        //activate the new colored bubble
        bubble.SetActive(true);
    }
    */

    

    public void ChangeColor()
    {


        // Randomly select a new colored bubble
        GameObject newColorBubble;
        do
        {
            randomColor = Random.Range(0, coloredBubbles.Length);
            newColorBubble = coloredBubbles[randomColor];
        } while (newColorBubble.tag == currentColorBubble?.tag); // be careful with the ? here very important to avoid null reference errors

        //bool canBreak = bubblesScript.ColorCheck(newColorBubble);// calling the BubbleCheck from Bubbles Script (lets hope it works)
        

        // Reuse the existing colored bubble or instantiate a new one if not already present
        GameObject bubble = currentColorBubble;
        if (bubble == null || bubble.tag != newColorBubble.tag)
        {
            if (bubble != null)
            {
                Destroy(bubble); // Destroy the previous bubble if it exists
            }

            bubble = Instantiate(newColorBubble, pos, Quaternion.identity);
            bubble.transform.parent = decider.transform;
        }
        else
        {
            bubble.transform.parent = decider.transform;
            bubble.SetActive(true);
        }

        // Set the new colored bubble as the current one
        currentColorBubble = bubble;
    }


}
