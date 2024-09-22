using UnityEngine;

public class BoardTile : MonoBehaviour
{
    private Renderer tileRenderer;

    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
        tileRenderer.enabled = false;  // Hidden by default
    }

    public void ShowTile()
    {
        tileRenderer.enabled = true;
    }

    public void HideTile()
    {
        tileRenderer.enabled = false;
    }
}

