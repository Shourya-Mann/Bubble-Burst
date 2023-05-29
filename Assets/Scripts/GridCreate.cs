using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreate : MonoBehaviour
{
    public GameObject backTile; // Reference to the background tile prefab or GameObject
    public int gridRows; // Number of rows in the grid
    public int gridColumns; // Number of columns in the grid
    public float tileWidth; // Width of each tile
    public float tileHeight; // Height of each tile

    void Start()
    {
        Vector3 initialTilePosition = new Vector3(-2.16f, 4.42f, -0.4108993f);

        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {
                // Calculate the position for each tile based on row and column indices
                float posX = initialTilePosition.x + (col * tileWidth);
                float posY = initialTilePosition.y - (row * tileHeight);
                Vector3 tilePosition = new Vector3(posX, posY, initialTilePosition.z);

                // Instantiate a new tile or clone the background tile at the calculated position
                GameObject newTile = Instantiate(backTile, tilePosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
