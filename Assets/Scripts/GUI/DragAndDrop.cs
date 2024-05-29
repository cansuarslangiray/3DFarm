using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool _isDragging = false;
    private Vector3 _offset;

    private void OnMouseDown()
    {
        _isDragging = true;
        _offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (_isDragging)
        {
            transform.position = GetMouseWorldPos() + _offset;
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