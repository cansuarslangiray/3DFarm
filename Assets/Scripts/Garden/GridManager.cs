using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    private GameObject gridCellPrefab; 
    public float cellSize = 1.0f;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, 0, y * cellSize);
                Instantiate(gridCellPrefab, cellPosition, Quaternion.identity, transform);
            }
        }
    }
   

    public void SelectObject(GameObject gameObject)
    {
        gridCellPrefab = gameObject;
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 gridPosition = hit.point - new Vector3(0.5f * cellSize, 0, 0.5f * cellSize);
                Vector3 alignedPosition = new Vector3(
                    Mathf.Floor(gridPosition.x / cellSize) * cellSize,
                    0,
                    Mathf.Floor(gridPosition.z / cellSize) * cellSize
                );
                PlaceObject(alignedPosition);
            }
        }
    }

    void PlaceObject(Vector3 position)
    {
        Instantiate(gridCellPrefab, position, Quaternion.identity); // Nesne yerleştirme
    }
}
