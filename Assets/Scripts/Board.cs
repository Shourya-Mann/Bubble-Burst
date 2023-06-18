using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState// it can have certain different states for this game we have wait and move but wecan create stuff like booster on etc.
{
    refill,// set up in candy class
    play // set up in this class
    
}

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public GameObject[] bubbles;
    private BackgroundTile[,] allTiles;
    public GameObject[,] allBubble;

    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[width, height];
        allBubble = new GameObject[width, height];
        Setup();
    }

    
private void Setup()
{
    for (int column = 0; column < width; column++)
    {
        for (int row = 0; row < height; row++)
        {
            Vector2 tempPos = new Vector2(column, row);
            GameObject backTile = Instantiate(tilePrefab, tempPos, Quaternion.identity);
            backTile.transform.parent = transform;
            backTile.name = "(" + column + ", " + row + ")";

            int toUse = Random.Range(0, bubbles.Length);
            GameObject bubble = Instantiate(bubbles[toUse], tempPos, Quaternion.identity);
            bubble.transform.parent = backTile.transform;
            bubble.name = "(" + column + ", " + row + ")";
            allBubble[column, row] = bubble;
        }
    }
}

    





    public void BreakBubble(float column, float row)
    {
        // Destroy the bubble GameObject
        int columnIndex = Mathf.RoundToInt(column);
        int rowIndex = Mathf.RoundToInt(row);
        GameObject bubbleObject = allBubble[columnIndex, rowIndex];
        Destroy(bubbleObject);
        //bubbleObject = null;
        Debug.Log("Bubble Destroyed!");

        // Start the coroutine to refill the board
        StartCoroutine(RefillBoardCoroutine());
    }


    private void RefillSlot(int column, int row)
    {
        if (allBubble[column, row] == null) // Add null check to prevent overwriting existing bubbles
        {
            int toUse = Random.Range(0, bubbles.Length);
            GameObject newBubble = Instantiate(bubbles[toUse], GetPosition(column, row), Quaternion.identity);
            allBubble[column, row] = newBubble;
        }
    }


    private Vector2 GetPosition(int column, int row)
    {
        // Assuming each bubble has a fixed size of 1 unit
        float bubbleSize = 1f;
        Vector2 originPosition = Vector2.zero; // Set the origin position of your grid

        float x = originPosition.x + column * bubbleSize;
        float y = originPosition.y + row * bubbleSize;
        

        return new Vector2(x, y);
    }

    /*
    private IEnumerator RefillBoardCoroutine()
    {
        yield return new WaitForSeconds(2f);

        for (int columnIndex = width - 1; columnIndex >= 0; columnIndex--)
        {
            int emptySlotCount = 0;

            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                if (allBubble[columnIndex, rowIndex] == null)
                {
                    emptySlotCount++;
                }
                else if (emptySlotCount > 0)
                {
                    GameObject bubble = allBubble[columnIndex, rowIndex];
                    allBubble[columnIndex, rowIndex] = null;
                    allBubble[columnIndex, rowIndex - emptySlotCount] = bubble;
                    bubble.transform.position = GetPosition(columnIndex, rowIndex - emptySlotCount);
                }
            }

            for (int i = 0; i < emptySlotCount; i++)
            {
                yield return new WaitForSeconds(0f);
                RefillSlot(columnIndex, height - 1 - i);
            }
        }

        yield return new WaitForSeconds(0.5f);

        // Enable player input or perform other necessary actions
    }
    */

    // rewriting refill board co to deal with bubbles breaking in multiple columns
    private IEnumerator RefillBoardCoroutine()
    {
        yield return new WaitForSeconds(2f);

        for (int columnIndex = 0; columnIndex < width; columnIndex++)
        {
            int emptySlotCount = 0;

            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                if (allBubble[columnIndex, rowIndex] == null)
                {
                    emptySlotCount++;
                }
                else if (emptySlotCount > 0)
                {
                    GameObject bubble = allBubble[columnIndex, rowIndex];
                    allBubble[columnIndex, rowIndex] = null;
                    allBubble[columnIndex, rowIndex - emptySlotCount] = bubble;

                    if (bubble != null)
                    {
                        bubble.transform.position = GetPosition(columnIndex, rowIndex - emptySlotCount);
                    }
                }
            }

            for (int i = height - emptySlotCount; i < height; i++)
            {
                yield return new WaitForSeconds(0.5f);
                RefillSlot(columnIndex, i);
            }
        }

        yield return new WaitForSeconds(0f);

        // Enable player input or perform other necessary actions
    }




}
