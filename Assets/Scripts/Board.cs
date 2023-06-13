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
        for (int row = 0; row < width; row++)
        {
            for (int column = 0; column < height; column++)
            {
                Vector2 tempPos = new Vector2(row, column);
                GameObject backTile = Instantiate(tilePrefab, tempPos, Quaternion.identity);
                backTile.transform.parent = transform;
                backTile.name = "(" + row + ", " + column + ")";

                int toUse = Random.Range(0, bubbles.Length);
                GameObject bubble = Instantiate(bubbles[toUse], tempPos, Quaternion.identity);
                bubble.transform.parent = backTile.transform;
                bubble.name = "(" + row + ", " + column + ")";
                allBubble[row, column] = bubble;
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
        // Instantiate a new bubble object and assign it to the appropriate slot
        int toUse = Random.Range(0, bubbles.Length);
        GameObject newBubble = Instantiate(bubbles[toUse], GetPosition(column, row), Quaternion.identity);
        allBubble[column, row] = newBubble;
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


    private IEnumerator RefillBoardCoroutine()
    {
        // Wait for a short delay (optional, for visual effect)
        yield return new WaitForSeconds(0.5f);

        // Loop through each column
        for (int columnIndex = 0; columnIndex < height; columnIndex++)
        {
            // Create a list to store the bubbles that need to move down
            List<GameObject> bubblesToMove = new List<GameObject>();

            // Loop through each row from bottom to top
            for (int rowIndex = 0; rowIndex < width; rowIndex++)
            {
                // Check if the current slot is empty
                if (allBubble[columnIndex, rowIndex] == null)
                {
                    // Add the bubbles above the empty slot to the list
                    for (int i = rowIndex + 1; i < width; i++)
                    {
                        GameObject bubble = allBubble[columnIndex, i];
                        if (bubble != null)
                        {
                            bubblesToMove.Add(bubble);
                            allBubble[columnIndex, i] = null;
                        }
                    }
                }
            }

            // Move the bubbles down
            for (int i = 0; i < bubblesToMove.Count; i++)
            {
                int targetRow = width - 1 - i;
                allBubble[columnIndex, targetRow] = bubblesToMove[i];
                bubblesToMove[i].transform.position = GetPosition(columnIndex, targetRow);
            }

            // Refill the remaining empty slots with new bubbles
            for (int rowIndex = 0; rowIndex < width; rowIndex++)
            {
                if (allBubble[columnIndex, rowIndex] == null)
                {
                    RefillSlot(columnIndex, rowIndex);
                }
            }
        }

        // Optional: Wait for a short delay before enabling player input
        yield return new WaitForSeconds(0.5f);

        // Enable player input or perform other necessary actions
    }
    
}
