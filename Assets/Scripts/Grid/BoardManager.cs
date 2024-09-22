using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int rows = 8;
    public int columns = 8;
    public GameObject tilePrefab; // Tile prefab'�
    public Transform boardContainer; // Tahta i�in container

    public GameObject unitSlotPrefab; // Envanter slotu prefab'�
    public Transform inventoryContainer; // Envanter i�in container

    // Hexagon grid ayarlar�
    public float hexWidth = 1f;
    public float hexHeight = 1f;
    public float rowOffset = 0.5f; // S�ralar aras� ofset
    public Vector2 gridSpacing = new Vector2(1f, 1f); // Gridler aras� mesafe

    // Envanter ayarlar�
    public float inventoryYOffset = 2f; // Envanterin tahtadan ne kadar altta olaca��

    void Start()
    {
        CreateBoard();
        CreateInventory();
    }

    void CreateBoard()
    {
        // Tahtay� ortalamak i�in pozisyon hesaplama
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
        // S�ra ofsetini uyguluyoruz
        float xOffset = (row % 2 == 0) ? 0 : hexWidth * rowOffset;
        float xPos = col * hexWidth * gridSpacing.x + xOffset;
        float zPos = row * hexHeight * gridSpacing.y * 0.75f; // Hexagonlar�n �st �ste binmemesi i�in

        return new Vector3(xPos, 0, zPos);
    }

    Vector3 CalculateBoardCenterOffset()
    {
        // Ortalamak i�in tahtan�n yar�s�n� hesapla
        float boardWidth = columns * hexWidth * gridSpacing.x;
        float boardHeight = rows * hexHeight * gridSpacing.y * 0.75f;
        return new Vector3(-boardWidth / 2f, 0, -boardHeight / 2f);
    }

    void CreateInventory()
    {
        // 9'luk envanter slotu olu�turma (�rne�in TFT tarz� bir s�ra)
        float inventoryWidth = 9 * hexWidth * gridSpacing.x;
        float startX = -(inventoryWidth / 2f); // Ortaya hizalama
        Vector3 inventoryStartPos = new Vector3(0, 0, -rows * hexHeight * gridSpacing.y * 0.5f - inventoryYOffset); // Envanteri tahtan�n alt�na yerle�tir

        for (int i = 0; i < 9; i++)
        {
            Vector3 position = new Vector3(startX + i * hexWidth * gridSpacing.x, 0, inventoryStartPos.z);
            GameObject newSlot = Instantiate(unitSlotPrefab, position, Quaternion.identity, inventoryContainer);
            newSlot.name = $"InventorySlot_{i}";
        }
    }
}
