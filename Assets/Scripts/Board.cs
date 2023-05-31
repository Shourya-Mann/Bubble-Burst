using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public int width;
    public int height;
    public GameObject tilePrefab;
    private BackgroundTile[,] allTiles;
    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[width, height];
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

            }
        }
    }
}
