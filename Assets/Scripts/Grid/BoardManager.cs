using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int rows = 8;
    public int columns = 8;
    public GameObject tilePrefab; // Tile prefab'ý
    public Transform boardContainer; // Tahta için container

    public GameObject unitSlotPrefab; // Envanter slotu prefab'ý
    public Transform inventoryContainer; // Envanter için container

    void Start()
    {
        CreateBoard();
        CreateInventory();
    }

    void CreateBoard()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(col, 0, row);
                GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity, boardContainer);
                newTile.name = $"Tile_{row}_{col}";
            }
        }
    }

    void CreateInventory()
    {
        // Oyuncu envanteri için 9 slot (3x3 gibi)
        for (int i = 0; i < 9; i++)
        {
            GameObject newSlot = Instantiate(unitSlotPrefab, inventoryContainer);
            newSlot.name = $"InventorySlot_{i}";
        }
    }
}
