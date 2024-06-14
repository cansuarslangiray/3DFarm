using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool _isDragging = false;
    public Vector3 _offset;
    public float groundLevelY = 8.5f; 

    private void Start()
    {
        Vector3 initialPosition = transform.position;
        initialPosition.y = groundLevelY;
        transform.position = initialPosition;
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        _offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (_isDragging)
        {
            Vector3 newPosition = GetMouseWorldPos() + _offset;
            newPosition.y = groundLevelY; 
            transform.position = newPosition;
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}