using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ICameraInput
{
    [HideInInspector] public Vector2 TouchDist;
    [HideInInspector] public Vector2 PointerOld;
    [HideInInspector] public bool Pressed;
    
    private int _pointerId;

    private void Update()
    {
        if (Pressed)
        {
            if (_pointerId >= 0 && _pointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[_pointerId].position - PointerOld;
                PointerOld = Input.touches[_pointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        _pointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

    public Vector2 GetInput()
    {
        return TouchDist;
    }
}
