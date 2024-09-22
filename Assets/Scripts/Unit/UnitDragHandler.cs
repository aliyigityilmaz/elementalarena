using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private bool isDragging = false;
    public bool isPreparationPhase = true;  // Bu oyun a�amas�na g�re de�i�ecek
    private Transform parentToReturnTo = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPreparationPhase)
        {
            isDragging = true;
            startPosition = transform.position;
            parentToReturnTo = transform.parent;
            transform.SetParent(null);  // Ge�ici olarak parent'� kald�r�yoruz
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
            Transform droppedTile = GetDroppedTileUnderUnit();
            if (droppedTile != null)
            {
                transform.position = droppedTile.position;
                transform.SetParent(droppedTile);
            }
            else
            {
                // Ge�ersiz bir yere b�rak�ld�ysa ba�lang�� pozisyonuna d�n
                transform.position = startPosition;
                transform.SetParent(parentToReturnTo);
            }
        }
        else
        {
            // Sava� a�amas�nda unit envantere geri d�ner
            transform.position = startPosition;
            transform.SetParent(parentToReturnTo);
        }
        isDragging = false;
    }

    Transform GetDroppedTileUnderUnit()
    {
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