using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                GameObject BackTile =  Instantiate(tilePrefab, tempPos, Quaternion.identity) as GameObject;
                BackTile.transform.parent = this.transform;
                // lets also get the postions for the BackTile for sanity Checks
                BackTile.name = "(" + row + " ," + column + ")";
                int toUse = Random.Range(0, bubbles.Length);
                GameObject bubble = Instantiate(bubbles[toUse], tempPos, Quaternion.identity);
                bubble.transform.parent = this.transform; // bubble is the child of the backtile
                bubble.name = "(" + row + " ," + column + ")"; // sanity check
                allBubble[row, column] = bubble;
            }
        }
    }
}

