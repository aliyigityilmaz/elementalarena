using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    public int width = 7;  // Number of columns
    public int height = 6; // Number of rows
    public float tileSize = 1.0f; // Distance between centers of adjacent tiles
    public GameObject hexTilePrefab;

    private GameObject[,] gridArray;

    void Start()
    {
        CreateHexGrid();
        ToggleTileVisibility(false); // Initially, all tiles are hidden
    }

    void CreateHexGrid()
    {
        gridArray = new GameObject[width, height];

        float xOffset = tileSize * 0.75f; // Horizontal distance between hex tiles
        float yOffset = tileSize * Mathf.Sqrt(3) / 2; // Vertical distance between hex tiles

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float xPos = x * xOffset;

                // Offset every other row to create staggered rows
                if (y % 2 == 1)
                {
                    xPos += xOffset / 2;
                }

                float yPos = y * yOffset;

                // Calculate position
                Vector3 position = new Vector3(xPos, 0, yPos);

                // Instantiate the hex tile but make it invisible
                GameObject tile = Instantiate(hexTilePrefab, position, Quaternion.identity);
                tile.GetComponent<Renderer>().enabled = false; // Initially hide the tile
                gridArray[x, y] = tile;
            }
        }
    }

    public void ToggleTileVisibility(bool isVisible)
    {
        foreach (GameObject tile in gridArray)
        {
            tile.GetComponent<Renderer>().enabled = isVisible;
        }
    }
}
