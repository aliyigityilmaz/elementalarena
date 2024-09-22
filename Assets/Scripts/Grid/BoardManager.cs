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

    // Hexagon grid ayarlarý
    public float hexWidth = 1f;
    public float hexHeight = 1f;
    public float rowOffset = 0.5f; // Sýralar arasý ofset
    public Vector2 gridSpacing = new Vector2(1f, 1f); // Gridler arasý mesafe

    // Envanter ayarlarý
    public float inventoryYOffset = 2f; // Envanterin tahtadan ne kadar altta olacaðý

    void Start()
    {
        CreateBoard();
        CreateInventory();
    }

    void CreateBoard()
    {
        // Tahtayý ortalamak için pozisyon hesaplama
        Vector3 boardCenterOffset = CalculateBoardCenterOffset();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Hexagonal pozisyon hesaplama
                Vector3 position = CalculateHexPosition(row, col) + boardCenterOffset;
                GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity, boardContainer);
                newTile.name = $"Tile_{row}_{col}";
            }
        }
    }

    Vector3 CalculateHexPosition(int row, int col)
    {
        // Sýra ofsetini uyguluyoruz
        float xOffset = (row % 2 == 0) ? 0 : hexWidth * rowOffset;
        float xPos = col * hexWidth * gridSpacing.x + xOffset;
        float zPos = row * hexHeight * gridSpacing.y * 0.75f; // Hexagonlarýn üst üste binmemesi için

        return new Vector3(xPos, 0, zPos);
    }

    Vector3 CalculateBoardCenterOffset()
    {
        // Ortalamak için tahtanýn yarýsýný hesapla
        float boardWidth = columns * hexWidth * gridSpacing.x;
        float boardHeight = rows * hexHeight * gridSpacing.y * 0.75f;
        return new Vector3(-boardWidth / 2f, 0, -boardHeight / 2f);
    }

    void CreateInventory()
    {
        // 9'luk envanter slotu oluþturma (örneðin TFT tarzý bir sýra)
        float inventoryWidth = 9 * hexWidth * gridSpacing.x;
        float startX = -(inventoryWidth / 2f); // Ortaya hizalama
        Vector3 inventoryStartPos = new Vector3(0, 0, -rows * hexHeight * gridSpacing.y * 0.5f - inventoryYOffset); // Envanteri tahtanýn altýna yerleþtir

        for (int i = 0; i < 9; i++)
        {
            Vector3 position = new Vector3(startX + i * hexWidth * gridSpacing.x, 0, inventoryStartPos.z);
            GameObject newSlot = Instantiate(unitSlotPrefab, position, Quaternion.identity, inventoryContainer);
            newSlot.name = $"InventorySlot_{i}";
        }
    }
}
