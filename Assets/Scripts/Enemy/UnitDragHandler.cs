using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private bool isDragging = false;
    public bool isPreparationPhase = true;  // This should be updated based on game state
    private Unit unit;
    private Transform parentToReturnTo = null;

    void Start()
    {
        unit = GetComponent<Unit>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPreparationPhase || unit.isOnBoard == false)
        {
            isDragging = true;
            startPosition = transform.position;
            parentToReturnTo = transform.parent;
            transform.SetParent(null);  // Temporarily detach from grid or inventory
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 worldPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                GetComponent<RectTransform>(), eventData.position, Camera.main, out worldPosition);
            transform.position = worldPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPreparationPhase)
        {
            // Place unit on the board if valid
            Transform droppedTile = GetDroppedTileUnderUnit();
            if (droppedTile != null)
            {
                transform.position = droppedTile.position;  // Snap to tile position
                transform.SetParent(droppedTile);  // Set the unit as a child of the tile
                unit.isOnBoard = true;
            }
            else
            {
                // If not dropped on a valid tile, return to original position
                transform.position = startPosition;
                transform.SetParent(parentToReturnTo);
            }
        }
        else
        {
            // Return to inventory if battle phase
            transform.position = startPosition;
            transform.SetParent(parentToReturnTo);
        }
        isDragging = false;
    }

    Transform GetDroppedTileUnderUnit()
    {
        // Raycast logic to detect if the unit is over a valid tile on the grid
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                return hit.collider.transform;
            }
        }
        return null;
    }
}